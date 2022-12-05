using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [Header("Settings")]
    private float healTime = 4f;

    [Header("Object References")]
    private FirstPersonController playerController;
    private ItemSwitching itemHandler;
    private Animator playerAnimator;
    public bool isHealing = false;

    [Header("Audio")]
    private AudioSource medKitAudioSource = default;
    [SerializeField] private AudioClip zipper = default;
    [SerializeField] private AudioClip bandage = default;

    void Start()
    {
        playerController = GetComponentInParent<FirstPersonController>();
        playerAnimator = playerController.GetComponentInChildren<Animator>();
        itemHandler = GetComponentInParent<ItemSwitching>();
        medKitAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale > 0.9 && !playerController.isDead)
        {
            // Heal
            if (Input.GetButtonDown("Fire1") && !isHealing && !itemHandler.GetComponent<ItemSwitching>().isSwitching)
            {
                StartCoroutine(Heal());
            }
        }
    }

    void OnEnable()
    {
        // Handles the case where you switch items while reloading
        isHealing = false;
    }

    private IEnumerator Heal()
    {
        isHealing = true;
        playerAnimator.SetBool("Healing", true);
        playerAnimator.SetBool("Switching", true);

        // Wait for hand to reach MedKit then play zipper sound effect
        yield return new WaitForSeconds(0.5f);
        medKitAudioSource.PlayOneShot(zipper);

        // Wait for zipper sound effect to stop then play bandage sound effect
        yield return new WaitForSeconds(0.5f);
        medKitAudioSource.PlayOneShot(bandage);

        // Heals the player once animation is done
        yield return new WaitForSeconds(healTime - 2f);
        playerController.AddHealth(50);

        // Finds the MedKit position in the player's inventory
        int medKitSlot = 0 ;
        for(int i = 0; i < 8; i++)
        {
            if (playerController.inventoryItems[i] == "MedKit") medKitSlot = i;
        }

        // Removes a MedKit from the player's inventory
        playerController.RemoveInventoryItem("MedKit", 1);

        // Equips next item
        playerController.GetComponentInChildren<ItemSwitching>().NoRemaingingItemsFindNext();

        playerAnimator.SetBool("Healing", false);
        isHealing = false;
    }
}