using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearStatueInteract : Interactable
{
    [SerializeField] GameObject player;
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
            onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Gave the bear the " + KeyName + " and it dropped something...");
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
                onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Hmm looks like something can fit in the bears mouth..." +
                    "\nMaybe there is something in the kitchen. ");
            }
        }
    }

    public override void OnLoseFocus()
    {
        
    }
}
