using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitInBathroomSpawnZombieScript : Interactable
{
    [SerializeField] GameObject onScreenUI;
    [SerializeField] GameObject hallwayZombie;
    [SerializeField] GameObject foyerDoor;
    [SerializeField] GameObject firstPersonController;
    [SerializeField] GameObject primaryCandle;
    [SerializeField] GameObject[] secondaryCandles;

    [SerializeField] GameObject medKit;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip creepySound;
    [SerializeField] AudioClip gustSound;
    private bool creepySoundPlayed = false, gustSoundPlayed = false;
    private float timer = 0;
    

    private void Update()
    {
        if (timer != 0)
        {
            switch(Time.time - timer)
            {
                case < 2: //nothing happens for two seconds
                    break;
                case < 6:
                    //play a creepy sound and blow out the other candles.
                    if (!creepySoundPlayed)
                    {
                        audioSource.clip = creepySound;
                        audioSource.Play();
                        creepySoundPlayed = true;

                        for(int i = 0; i < secondaryCandles.Length; i++)
                        {
                            secondaryCandles[i].GetComponent<LightCandles>().forceLightingState(false);
                        }
                    }
                    
                    break;
                case < 7:
                    //play gust sound and blow out candle.
                    if (!gustSoundPlayed)
                    {
                        audioSource.clip = gustSound;
                        audioSource.Play();
                        gustSoundPlayed = true;
                    }
                    break;

                case < 13:
                    primaryCandle.GetComponent<LightCandles>().forceLightingState(false);
                    break;
                default:
                   //gameObject.SetActive(false); //deactivate gameobject after all events are complete
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
        foyerDoor.GetComponent<Door>().isOpen = true;


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
