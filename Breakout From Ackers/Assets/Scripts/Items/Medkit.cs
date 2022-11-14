using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float healTime = 4f;

    [SerializeField] private FirstPersonController playerController;
    private Animator playerAnimator;
    private bool isHealing = false;

    void Start()
    {
        playerAnimator = playerController.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Heal
        if (Input.GetButtonDown("Fire1") && !isHealing)
        {
            StartCoroutine(Heal());
        }
    }

    private IEnumerator Heal()
    {
        isHealing = true;
        playerAnimator.SetBool("Healing", true);

        // Heals the player
        yield return new WaitForSeconds(healTime - 0.5f);
        playerController.AddHealth(50);

        // Finds the MedKit position in the player's inventory
        int medKitSlot = 0 ;
        for(int i = 0; i < 8; i++)
        {
            if (playerController.inventoryItems[i] == "MedKit") medKitSlot = i;
        }

        // Removes a MedKit from the player's inventory
        playerController.inventoryItemsCount[medKitSlot]--;

        

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

        // TODO: Tune coroutine and animation time

        //yield return new WaitForSeconds(0.5f);

        isHealing = false;
    }
}