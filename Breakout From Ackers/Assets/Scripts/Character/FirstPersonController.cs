using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FirstPersonController : CharacterStats
{
    #region Parameters and Variables
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKey(jumpKey) && characterController.isGrounded;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    private AudioSource playerAudioSource;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool useFootsteps = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useStamina = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Additional Movement Parameters")]
    [SerializeField] private float crouchSpeed = 1.5f;
    private string currentMovement;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 6.0f;
    [SerializeField] private float gravity = 30.0f;
    private bool justLanded = false;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Additional Health Parameters")]
    public static Action<float> OnHeal;
    [SerializeField] private Transform respawnPoint;
    public bool isDead;

    [Header("Stamina Parameters")]
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaUseMultiplier = 20;
    [SerializeField] private float timeBeforeStaminaRegen = 5;
    [SerializeField] private float staminaValueIncrement = 2;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentStamina = 100;
    private Coroutine regeneratingStamina;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioClip[] footstepClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;

    [Header("Interaction Parameters")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    [Header("Inventory Parameters")]
    [SerializeField] public string[] inventoryItems = new string[8];
    [SerializeField] public int[] inventoryItemsCount = new int[8];
    [SerializeField] public string[] inventoryUnstackableItems;
    private int inventorySpacesCurrentlyUsed;
    private string currentItem;
    private bool craftedBlueMass = false;
    [SerializeField] private AudioClip craftBlueMassAmmoSound = default;

    [Header("Health And Debuff Parameters")]
    [SerializeField] private int Score;
    [SerializeField] GameObject gameOver;
    //Global Variable
    private int GotHitValue = -5;
    private int timeAdjuster = 5;
    private int healDDScore = -10;
    private int deathDDScore = -10;
    float timeOfLastHit = 0;
    [SerializeField] private float walkSpeedModifer = 1.5f;
    [SerializeField] private float runSpeedModifer = 1f;
    [SerializeField] private AudioClip[] gruntClips = default;
    [SerializeField] private AudioClip debuffGrunt = default;
    [SerializeField] private AudioClip deathGrunt = default;
    //Difficulty adjustment based on time
    private float deltaTime = 150;
    private float lastTimeAdjust;

    private Camera playerCamera;
    private CharacterController characterController;
    private Animator playerAnimations;

    public bool isDebuffed = false;
    private float debuffTimer = 0;
    [SerializeField] private float debuffDuration;
    //--------end of health and debuff-------------------
    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [SerializeField] GameObject onscreenUI;
    private GameObject[] padlockCameras;
    [SerializeField] GameObject mainCamera;

    #endregion

    #region Awake and Update


    void Awake()
    {
        // Cache components
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        playerAnimations = GetComponentInChildren<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        defaultYPos = playerCamera.transform.localPosition.y;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Difficulty Adjustment Initialized
        Score = 100;
        lastTimeAdjust = 0;

        //Set Inventory Item Count
        inventorySpacesCurrentlyUsed = InitializeInventorySpaceCurrentlyUsed();

        // Player is alive
        isDead = false;
    }

    void Update()
    {
        if(playerCamera.isActiveAndEnabled) currentItem = GetComponentInChildren<ItemSwitching>().getCurrentItemName();
        
        // Script works only when the game is unpaused
        if (Time.timeScale > 0.9 && !isDead && playerCamera.isActiveAndEnabled)
        {
            if (CanMove)
            {
                HandleMovementInput();
                HandleMouseLook();

                if (canJump) HandleJump();

                if (canCrouch) HandleCrouch();

                if (canUseHeadbob) HandleHeadbob();

                if (useFootsteps) HandleFootsteps();

                if (useStamina) HandleStamina();

                if (canInteract)
                {
                    HandleInteractionCheck();
                    HandleInteractionInput();
                }

                HandleAnimations();
                //HandleGruntAudio();

                ApplyFinalMovements();
            }

            if (Time.time > lastTimeAdjust + deltaTime)
            {
                lastTimeAdjust = Time.time;
                scoreAdjustment(timeAdjuster);
            }
            if (Time.time > debuffTimer + debuffDuration && isDebuffed)
            {
                undoDebuff();
            }
            if(timeOfLastHit < 5)
            {
                timeOfLastHit += Time.deltaTime;
                Debug.Log(timeOfLastHit);
            }
        }
       
    }

    #endregion

    #region Primary Movement Functions
    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        if (isCrouching && currentInput != Vector2.zero)
            currentMovement = "Crouch Walking";
        else if (IsSprinting && currentInput != Vector2.zero)
            currentMovement = "Sprinting";
        else if (currentInput != Vector2.zero)
            currentMovement = "Walking";
        else
            currentMovement = "None";

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
        {
            moveDirection.y = jumpForce;
        }

        // If the player jumps and hits the ceiling
        if (characterController.collisionFlags == CollisionFlags.Above)
        {
            // Remove y velocity from jumping
            moveDirection.y = 0;

            // Set stepOffset to zero to prevent player moving and sticking to the ceiling
            characterController.stepOffset = 0;
        }
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }

        /*
         
        Prototype for hold crouch
         
        Add to crouching parameters: private Coroutine crouching;

         if (Input.GetKeyDown(crouchKey))
        {
            if(crouching != null)
            {
                StopCoroutine(crouching);
                crouching = null;
            }
            crouching = StartCoroutine(CrouchStand());
        }
        
        if (Input.GetKeyUp(crouchKey))
        {
            if(crouching != null)
            {
                StopCoroutine(crouching);
                crouching = null;
            }
            crouching = StartCoroutine(CrouchStand());
        }
         */
    }
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 2f))
        {
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensures exact values
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    #endregion

    #region Secondary Movement Functions
    private void HandleStamina()
    {
        if (IsSprinting && currentInput != Vector2.zero) // Sprinting and moving
        {
            if (regeneratingStamina != null) // Stop regenerating stamina if player starts sprinting
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }

            // Remove stamina while player is sprinting
            currentStamina -= staminaUseMultiplier * Time.deltaTime;

            // Don't let stamina go below 0
            if (currentStamina < 0) currentStamina = 0;

            // Stop the player from sprinting if they run out of stamina
            if (currentStamina <= 0) canSprint = false;
        }

        // If the player is not sprinting, not at max stamina, and isn't already regenerating stamina, start regenerating stamina
        if (!IsSprinting && currentStamina < maxStamina && regeneratingStamina == null) regeneratingStamina = StartCoroutine(RegenerateStamina());
    }

    private IEnumerator RegenerateStamina()
    {
        // Waits for given time to start regenerating stamina
        yield return new WaitForSeconds(timeBeforeStaminaRegen);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while(currentStamina < maxStamina)
        {
            // If any amount of stamina is there, allow the player to sprint
            if (currentStamina > 0) canSprint = true;

            // Add stamina based on increment amount
            currentStamina += staminaValueIncrement;

            // Don't let stamina go above 100
            if (currentStamina > maxStamina) currentStamina = maxStamina;

            yield return timeToWait;
        }

        // Stops the coroutine
        regeneratingStamina = null;
    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }

    }
    private void HandleFootsteps()
    {
        // If the player is on the ground
        if (characterController.isGrounded)
        {
            if (currentInput != Vector2.zero) // If they are moving, play a footstep for each step
            {
                footstepTimer -= Time.deltaTime;

                if (footstepTimer <= 0)
                {
                    PlayFootstep();

                    footstepTimer = GetCurrentOffset;
                }
            }
            if (justLanded) // If they just landed, play a footstep and mark justLanded as false
            {
                justLanded = false;
                footstepTimer -= Time.deltaTime;
                PlayFootstep();
                footstepTimer = GetCurrentOffset;
            }
        }
        else // If player isn't grounded, set justLanded to true to prepare for the landing on a surface
        {
            if(moveDirection.y > 1) justLanded = true;
        }
    }

    private void PlayFootstep()
    {
        if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 5))
        {
            playerAudioSource.PlayOneShot(footstepClips[UnityEngine.Random.Range(0, footstepClips.Length - 1)]);
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public string getCurrentMovement()
    {
        if (moveDirection.y > 0) return "Jumping";
        else return currentMovement;
    }

    #endregion

    #region Animation Functions

    private void HandleAnimations()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) // Standing Still
        {
            playerAnimations.SetFloat("Speed", 0);
        }
        else // Moving
        {
            if (isCrouching)
            {
                playerAnimations.SetFloat("Speed", 1);
            }
            else if (IsSprinting)
            {
                playerAnimations.SetFloat("Speed", 3);
            }
            else
            {
                playerAnimations.SetFloat("Speed", 2);
            }


        }

        if (currentItem == "Hands") // Nothing is equipped
        {
            playerAnimations.SetBool("Hands", true);
        }
        else
        {
            playerAnimations.SetBool("Hands", false);
        }

        if(currentItem == "M1911") // M1911 is equipped
        {
            playerAnimations.SetBool("M1911", true);
        }
        else
        {
            playerAnimations.SetBool("M1911", false);
        }

        if (currentItem == "MedKit") // MedKit is equipped
        {
            playerAnimations.SetBool("MedKit", true);
        }
        else
        {
            playerAnimations.SetBool("MedKit", false);
        }

        if (currentItem == "Blue Mass Pills") // Pills are equipped
        {
            playerAnimations.SetBool("Blue Mass Pills", true);
        }
        else
        {
            playerAnimations.SetBool("Blue Mass Pills", false);
        }

        if (currentItem == "Knife") // Knife is equipped
        {
            playerAnimations.SetBool("Knife", true);
        }
        else
        {
            playerAnimations.SetBool("Knife", false);
        }
    }

    #endregion

    #region Interaction Functions

    // Constantly raycasts out to check for interactable objects
    private void HandleInteractionCheck()
    {
        //Draw a line to see ray length on scene
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance);
        // Checks for any object the player is looking at
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            // If the object we are looking at is a new interactable object
            if (hit.collider.gameObject.layer == 11 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                // Gets the interactable object
                hit.collider.TryGetComponent(out currentInteractable);

                // Sets focus to the new interactable object
                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else if (currentInteractable) // If the raycast doesn't find an interactable object and we already have one stored
        {
            // Get rid of the stored interactable object since we are no longer looking at it
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    // Check for interact key being pressed
    private void HandleInteractionInput()
    {
        // If the interaction key is pressed and the player is looking at an interactable object, a raycast is sent from the camera with the set parameters checking for an interaction layer
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
        }
    }

    #endregion

    #region Health / Debuff Functions
    protected override void KillCharacter()
    {
        isDead = true;
        scoreAdjustment(deathDDScore);

        currentHealth = 0;

        undoDebuff();

        gameOver.SetActive(true);
        gameOver.GetComponent<GameOverScript>().timePassed = Time.time;
        gameOver.GetComponent<GameOverScript>().movePlayerOutOfThePlaySpace();

        
        
        padlockCameras = GameObject.FindGameObjectsWithTag("Padlock Camera");
        mainCamera.SetActive(true);
        for(int i = 0; i < padlockCameras.Length; i++)
        {
            padlockCameras[i].SetActive(false);
            Debug.Log(i);
        }
    }

    protected override void ApplyDamage(float dmg)
    {
        if(timeOfLastHit > 3)
        {
            Debug.Log("Reset dmg timer");
            timeOfLastHit = 0f;
            currentHealth -= dmg;
            OnDamage?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                PlayGruntSound(3);
                KillCharacter();
            }
            else
            {
                PlayGruntSound(1);
            }

        }

        
            
    }
    public void AddHealth(float healAmt)
    {
        currentHealth += healAmt;
        //They healed game to hard;
        scoreAdjustment(healDDScore);
        
        if (currentHealth >= 100) currentHealth = 100;
    }
    public void doDamage(float dmg)
    {
        //Do damage
        ApplyDamage(dmg * diffcultyValue());
        //Adjust Score after getting hit;
        scoreAdjustment(GotHitValue);
    }

    public void debuffPlayer()
    {
        //Only debuff when not debuffed
        if (!isDebuffed)
        {
            PlayGruntSound(2);
            isDebuffed = true;
            walkSpeed -= walkSpeedModifer;
            sprintSpeed -= runSpeedModifer;
            debuffTimer = Time.time;
        }
    }
    public void undoDebuff()
    {
        //Revert change from debuff else just skip
        if (isDebuffed)
        {
            walkSpeed += walkSpeedModifer;
            sprintSpeed += runSpeedModifer;
        }
        isDebuffed = false;
    }

    private void PlayGruntSound(int gruntSelect)
    {
        if (gruntSelect == 0) return;
        else if (gruntSelect == 1) playerAudioSource.PlayOneShot(gruntClips[UnityEngine.Random.Range(0, gruntClips.Length - 1)]);
        else if (gruntSelect == 2) playerAudioSource.PlayOneShot(debuffGrunt);
        else if (gruntSelect == 3) playerAudioSource.PlayOneShot(deathGrunt);

        Debug.Log(gruntSelect);

        gruntSelect = 0;
    }

    #endregion

    #region Difficulty Adjustment Functions
    public float diffcultyValue()
    {
        // The Player is doing well Penalize them
        if (Score > 120)
            return 1.2f;
        // The Player is doing normal Don't do anything
        else if (120 > Score && Score > 80)
            return 1f;
        // The Player is doing bad Help them
        else if (80 > Score)
            return .8f;

        //Should never get to here but 
        return 1f;
    }
    //This function changes the score via how many times a player heals and got hit
    public void scoreAdjustment(int value)
    {
        //Clamp the score
        Score = Score + value;
        if (Score > 131)
            Score = 130;
        if (Score < 79)
            Score = 80;
    }
    #endregion

    #region Inventory Functions
    public bool AddInventoryItem(string itemName, int itemCount)
    {
        if(itemName.CompareTo("Pistol Ammo") == 0 && craftedBlueMass)
        {
            itemName = "Blue Mass Ammo";
        }
        int spot = FindItemSpot(itemName);

        if(spot != -1 && IsStackable(itemName))
        {
            Debug.Log("Stacked item into pre-existing inventory.");
            inventoryItemsCount[spot] += itemCount;
            onscreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Picked up " + itemName + ".");
            return true;
        }
        else if( inventorySpacesCurrentlyUsed < 8)
        {
            Debug.Log("Added item to a new spot in the inventory.");
            int openSpot = findOpenSpot();
            inventoryItems[openSpot] = itemName;
            inventoryItemsCount[openSpot] = itemCount;
            inventorySpacesCurrentlyUsed++;
            onscreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Picked up " + itemName + ".");
            return true;
        }
        else
        {
            Debug.Log("No room in inventory to add another item.");
            onscreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Cannot pick up item. Inventory full.");
            return false;
        }
    }

    private int findOpenSpot()
    {
        for(int i = 0; i < 8; i++)
        {
            if (inventoryItems[i].CompareTo("") == 0)
            {
                return i;
            }
        }
        return -1;
    }

    //checks the inventory for an item corresponding to the string given and removes the item count given.
    //Returns a value corresponding to the amount removed from inventory. A zero return means none were in inventory.
    public int RemoveInventoryItem(string itemName, int itemCount)
    {
        int spot = FindItemSpot(itemName);
        if(spot == -1)
        {
            Debug.Log("Item was not found in inventory. Unable to remove from inventory.");
            return 0;
        }else if(inventoryItemsCount[spot] <= itemCount)
        {
            Debug.Log("Remove count met or exceeded current item count. Removed all possible.");
            int removeAmount = inventoryItemsCount[spot];
            inventoryItemsCount[spot] = 0;
            inventoryItems[spot] = "";
            inventorySpacesCurrentlyUsed--;
            //reorganizeInventory();
            return removeAmount;
        }
        else
        {
            Debug.Log("Removed " + itemCount + " " + itemName + " from inventory.");
            inventoryItemsCount[spot] -= itemCount;
            return itemCount;
        }        
    }

    private int FindItemSpot(string itemName)
    {
        int spot = -1;
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].CompareTo(itemName) == 0)
            {
                spot = i;
                break;
            }
        }
        if (spot == -1)
            Debug.LogWarning(itemName + " was not found in inventory.");
        return spot;
    }


    private bool IsStackable(string itemName)
    {
        for(int i = 0; i < inventoryUnstackableItems.Length; i++)
        {
            if (itemName.CompareTo(inventoryUnstackableItems[i]) == 0)
                return false;
        }
        return true;
    }

    private int InitializeInventorySpaceCurrentlyUsed()
    {
        int i = 0;
        for(; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].Length == 0)
                return i;
        }
        return i;
    }

    public void craftBlueMassAmmo()
    {
        playerAudioSource.PlayOneShot(craftBlueMassAmmoSound);
        craftedBlueMass = true;
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].CompareTo("Pistol Ammo") == 0)
                inventoryItems[i] = "Blue Mass Ammo";
        }
    }
    //removed function. We no longer reorganize the inventory. Leaving here in case we change it back.
    void reorganizeInventory()
    {
        for(int i = 0; i < 7; i++)
        {
            if(inventoryItems[i].CompareTo("") == 0)
            {
                inventoryItems[i] = inventoryItems[i + 1];
                inventoryItems[i + 1] = "";
                inventoryItemsCount[i] = inventoryItemsCount[i + 1];
                inventoryItemsCount[i + 1] = 0;
            }
        }
    }
    public int GetInventorySpaceCurrentlyUsed()
    {
        return inventorySpacesCurrentlyUsed;
    }
    #endregion
}
