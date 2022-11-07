using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitInBathroomSpawnZombieScript : Interactable
{
    [SerializeField] GameObject onScreenUI;
    [SerializeField] GameObject hallwayZombie;
    [SerializeField] GameObject foyerDoor;
    [SerializeField] GameObject firstPersonController;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Picked up MedKit.");
        firstPersonController.GetComponent<FirstPersonController>().AddInventoryItem("MedKit", 1);
        
        hallwayZombie.SetActive(true);
      
        foyerDoor.GetComponent<Door>().isLocked = false;
        foyerDoor.GetComponent<Door>().door.Play("Door2_Open");
        gameObject.SetActive(false);

    }

    public override void OnLoseFocus()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
