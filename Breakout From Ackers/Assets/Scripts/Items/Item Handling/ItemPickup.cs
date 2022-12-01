using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] int itemCount;
    [SerializeField] string itemName;
    [SerializeField] bool itemCountAffectedByScore;
    [SerializeField] private bool destroyOnInteract = true;
    [SerializeField] bool setObjective = false;
    [SerializeField] int objectiveNumber = 0;
    private bool alreadyPickedUp = false;
    private GameObject firstPersonController;
    private GameObject onScreenUI;
    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip itemPickupSound = default;

    private void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player");
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
        playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    public override void OnFocus()
    {
      
    }

    public override void OnInteract()
    {
       
        int adjustedItemCount = itemCount;

        //alters item pickup count dependent on player score if bool is true
        if (itemCountAffectedByScore)
        {
            float denominator = firstPersonController.GetComponent<FirstPersonController>().diffcultyValue();
            adjustedItemCount = (int)((float)adjustedItemCount / denominator);
        }

        if (setObjective && !alreadyPickedUp) onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(objectiveNumber);

        if (!alreadyPickedUp && firstPersonController.GetComponent<FirstPersonController>().AddInventoryItem(itemName, adjustedItemCount))
        {
            playerAudioSource.PlayOneShot(itemPickupSound);
            alreadyPickedUp = true;
            if (destroyOnInteract) Destroy(gameObject);
        } else if (alreadyPickedUp) onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("You already picked up everything available. It's empty now.");

    }

    public override void OnLoseFocus()
    {

    }

}
