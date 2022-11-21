using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearStatueInteract : Interactable
{
    [SerializeField] GameObject player, fish, itemDrop;
    FirstPersonController FPC;
    private GameObject onScreenUI;
    [SerializeField] private string KeyName;

    private void Start()
    {
        FPC = player.GetComponent<FirstPersonController>();
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (FPC.RemoveInventoryItem(KeyName, 1) == 1)
        {
            onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("The fish dislodged something it the bears mouth and it fell to the floor.");
            fish.SetActive(true);
            itemDrop.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
            //TODO:Play some kind of sound
        }
        else
        {
            if (KeyName.CompareTo("") == 0)
            {
                onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Never gonna occur but error bug");

            }
            else
            {
                onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("A large statue of a bear with a gaping mouth.\nSomething might fit in it.");
            }
        }
    }

    public override void OnLoseFocus()
    {
        
    }
}
