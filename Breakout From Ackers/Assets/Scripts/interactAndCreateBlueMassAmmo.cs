using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactAndCreateBlueMassAmmo : Interactable
{
    private GameObject onScreenUI, firstPersonController, bossObject;
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if(firstPersonController.GetComponent<FirstPersonController>().RemoveInventoryItem("Pure Blue Mass", 1) == 1)
        {
            onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(7);
            firstPersonController.GetComponent<FirstPersonController>().craftBlueMassAmmo();
            onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Crafted Blue Mass Ammo.");
            bossObject.GetComponent<BossStat>().craftedBlueMass = true;

        }
        else
        {
            onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("A large desk. Work can be done here with the right supplies.");
        }
    }

    public override void OnLoseFocus()
    {
    }

    void Start()
    {
        onScreenUI = GameObject.Find("On Screen UI");
        firstPersonController = GameObject.FindGameObjectWithTag("Player");
        bossObject = GameObject.Find("WorkingBoss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
