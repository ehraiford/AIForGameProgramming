using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float healTime = 4f;

    [Header("Object References")]
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private GameObject itemHandler;
    private Animator playerAnimator;
    public bool isHealing = false;

    [Header("Audio")]
    [SerializeField] private AudioSource medKitAudioSource = default;
    [SerializeField] private AudioClip zipper = default;
    [SerializeField] private AudioClip bandage = default;

    void Start()
    {
        playerAnimator = playerController.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Heal
        if (Input.GetButtonDown("Fire1") && !isHealing && !itemHandler.GetComponent<ItemSwitching>().isSwitching)
        {
            StartCoroutine(Heal());
        }
    }

    private IEnumerator Heal()
    {
        isHealing = true;
        playerAnimator.SetBool("Healing", true);

        // Wait for hand to reach MedKit then play zipper sound effect
        yield return new WaitForSeconds(0.5f);
        medKitAudioSource.PlayOneShot(zipper);

        // Wait for zipper sound effect to stop then play bandage sound effect
        yield return new WaitForSeconds(0.5f);
        medKitAudioSource.PlayOneShot(bandage);

        // Heals the player once animation is done
        yield return new WaitForSeconds(healTime - 1f);
        playerController.AddHealth(50);

        // Finds the MedKit position in the player's inventory
        int medKitSlot = 0 ;
        for(int i = 0; i < 8; i++)
        {
            if (playerController.inventoryItems[i] == "MedKit") medKitSlot = i;
        }

        // Removes a MedKit from the player's inventory
        playerController.RemoveInventoryItem("MedKit", 1);

        
        // Checks if the player runs out of MedKits
        if (playerController.inventoryItemsCount[medKitSlot] > 0) // Player has more MedKits
        {
            playerAnimator.SetBool("RemainingMedKit", true);
            playerAnimator.SetBool("Healing", false);
        }
        else // Player runs out of MedKits
        {
            playerAnimator.SetBool("RemainingMedKit", false);
            playerAnimator.SetBool("Healing", false);
            playerController.inventoryItems[medKitSlot] = "";
            playerController.GetComponentInChildren<ItemSwitching>().NoRemaingingItemsFindNext();
        }
        

       

        isHealing = false;
    }
}