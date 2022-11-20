using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
    [Header("Settings")]
    private float popTime = 3f;
    [SerializeField] private int pillDDAdjustment = -5;
    [Header("Object References")]
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private GameObject itemHandler;
    private Animator playerAnimator;
    public bool isPopping = false;

    [Header("Audio")]
    [SerializeField] private AudioSource pillsAudioSource = default;
    [SerializeField] private AudioClip capPop = default;
    [SerializeField] private AudioClip pillShake = default;
    [SerializeField] private AudioClip swallow = default;

    void Start()
    {
        playerAnimator = playerController.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Time.timeScale > 0.9)
        {
            // Pop pill
            if (Input.GetButtonDown("Fire1") && !isPopping && !itemHandler.GetComponent<ItemSwitching>().isSwitching)
            {
                StartCoroutine(Pop());
            }
        } 
    }

    void OnEnable()
    {
        // Handles the case where you switch items while using
        isPopping = false;
    }

    private IEnumerator Pop()
    {
        isPopping = true;
        playerAnimator.SetBool("Popping", true);

        // Wait for hands to be in position then play cap pop sound
        yield return new WaitForSeconds(0.5f);
        pillsAudioSource.PlayOneShot(capPop);

        // Wait for the cap pop to stop then play shake pill sound
        yield return new WaitForSeconds(0.5f);
        pillsAudioSource.PlayOneShot(pillShake);

        // Wait for the pill shake to stop and hand to reach face then play swallow sound
        yield return new WaitForSeconds(1f);
        pillsAudioSource.PlayOneShot(swallow);

        // Removes debuff the player once animation is done
        yield return new WaitForSeconds(popTime - 2.75f);
        playerController.undoDebuff();
        playerController.scoreAdjustment(pillDDAdjustment);
        // Finds the Blue Mass Pills position in the player's inventory
        int pillsSlot = 0;
        for (int i = 0; i < 8; i++)
        {
            if (playerController.inventoryItems[i] == "Blue Mass Pills") pillsSlot = i;
        }

        // Removes a pill from the player's inventory
        playerController.RemoveInventoryItem("Blue Mass Pills", 1);


        // Checks if the player runs out of pills
        if (playerController.inventoryItemsCount[pillsSlot] > 0) // Player has more MedKits
        {
            playerAnimator.SetBool("RemainingPills", true);
            playerAnimator.SetBool("Popping", false);
        }
        else // Player runs out of pills
        {
            playerAnimator.SetBool("RemainingPills", false);
            playerAnimator.SetBool("Popping", false);
            playerController.GetComponentInChildren<ItemSwitching>().NoRemaingingItemsFindNext();
        }

        isPopping = false;
    }
}