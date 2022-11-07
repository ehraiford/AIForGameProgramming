using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Animation door;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource LockedDoorSound;
    [SerializeField] private AudioSource unlockDoorSound;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private FirstPersonController FPC;
    [SerializeField] private string KeyName;
    private bool isOpen;
    private string objName;
    void Start()
    {
        FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        door = GetComponentInParent<Animation>();
        objName = door.name;
        isOpen = false;
        doorSound = GetComponentInParent<AudioSource>();
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {

        if (isLocked)
        {
            //Has key
            if(FPC.RemoveInventoryItem(KeyName, 1) == 1)
            {
                unlockDoorSound.Play();
                isLocked = false;
            }
            else
            {
                LockedDoorSound.Play();
            }
            //no Key
            
        }
        else
        {
            if (objName.Contains("Door1"))
                objName = "Door1";
            else if (objName.Contains("Door2"))
                objName = "Door2";
            if (!isOpen)
            {
                isOpen = true;
                Debug.Log("DOOR OPEN");
                door.Play(objName + "_Open");
                doorSound.Play();
            }
            else
            {
                isOpen = false;
                Debug.Log("DOOR Close");
                door.Play(objName + "_Close");
                doorSound.Play();
            }
        }
        
    }


    public override void OnLoseFocus()
    {
        
    }

}
