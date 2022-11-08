using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitInBathroomSpawnZombieScript : Interactable
{
    [SerializeField] GameObject onScreenUI;
    [SerializeField] GameObject hallwayZombie;
    [SerializeField] GameObject foyerDoor;
    [SerializeField] GameObject firstPersonController;
    [SerializeField] GameObject candle;
    [SerializeField] GameObject medKit;
    [SerializeField] GameObject creepySound;

    private float timer = 0;
    

    private void Update()
    {
        if (timer != 0)
        {
            switch(Time.time - timer)
            {
                case < 2: //nothing happens for two seconds
                    break;
                case < 12:
                    creepySound.SetActive(true);
                    break;
                case < 13:
                    candle.GetComponent<LightCandles>().forceLightingState(false);
                    break;


                default:
                    gameObject.SetActive(false); //deactivate gameobject after all events are complete
                    break;
            
                    
            }
        }
    }
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        //the traditional adding item to inventory.
        firstPersonController.GetComponent<FirstPersonController>().AddInventoryItem("MedKit", 1);
        
        hallwayZombie.SetActive(true); //activate Alberto, the first zombie
      
        foyerDoor.GetComponent<Door>().isLocked = false; //unlock door to foyer
        foyerDoor.GetComponent<Door>().door.Play("Door2_Open"); //open door to foyer
        foyerDoor.GetComponent<Door>().isOpen = false;


        medKit.SetActive(false);

        timer = Time.time; //tracks time item was picked up so we can do events a few seconds later.

    }

    public override void OnLoseFocus()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
