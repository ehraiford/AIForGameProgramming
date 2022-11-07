using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitInBathroomSpawnZombieScript : Interactable
{
    [SerializeField] GameObject onScreenUI;
    [SerializeField] GameObject hallwayZombie;
    [SerializeField] GameObject foyerDoor;

    public override void OnFocus()
    {
        Debug.Log("Onfocus");
    }

    public override void OnInteract()
    {
        Debug.Log("interacted with object");
        onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Picked up MedKit.");
    }

    public override void OnLoseFocus()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
