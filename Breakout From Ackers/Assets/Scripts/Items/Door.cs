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
    private GameObject onScreenUI;
    public bool isOpen;
    private string objName;
    void Start()
    {
        FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
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
                onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Unlocked door using the " + KeyName + ".");
                unlockDoorSound.Play();
                isLocked = false;
            }
            else
            {
                if(KeyName.CompareTo("") == 0)
                {
                    onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("The door is barred shut. Not even a key can open it.");

                }
                else
                {
                    onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("The door is locked. You need the " + KeyName + ".");
                }

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
                door.Play("Door2_Open");
                Debug.Log(objName.ToString());
                doorSound.Play();
            }
            else
            {
                isOpen = false;
                Debug.Log("DOOR Close");
                door.Play("Door2_Close");
                doorSound.Play();
            }
        }
        
    }


    public override void OnLoseFocus()
    {
        
    }
    //For enemy (mainly stalker boss) to open doors
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Reach") && !isOpen)
        {
            isOpen = true;
            Debug.Log("DOOR OPEN");
            door.Play("Door2_Open");
            Debug.Log(objName.ToString());
            doorSound.Play();
        }
    }

}
