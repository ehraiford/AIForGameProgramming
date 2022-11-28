using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Animation door;
    private AudioSource doorAudio;
    [SerializeField] private AudioClip doorUse;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioClip doorUnlock;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private FirstPersonController FPC;
    [SerializeField] private string KeyName;
    private GameObject onScreenUI;
    public bool isOpen;
    private string objName;
    GameObject obstruction;
    float time;
    void Start()
    {
        FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
        door = GetComponentInParent<Animation>();
        objName = door.name;
        isOpen = false;
        doorAudio = GetComponentInParent<AudioSource>();
        obstruction = transform.parent.GetChild(1).gameObject;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 1)
        {
            if (isLocked && isOpen)
            {
                isOpen = false;
                door.Play("Door2_Close");
                doorAudio.PlayOneShot(doorUse);
            }
        }
        
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
                doorAudio.PlayOneShot(doorUnlock);
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

                doorAudio.PlayOneShot(doorLocked);
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
                door.Play("Door2_Open");
                doorAudio.PlayOneShot(doorUse);
            }
            else
            {
                isOpen = false;
                door.Play("Door2_Close");
                doorAudio.PlayOneShot(doorUse);
            }
        }
        obstruction.SetActive(!isOpen);
    }


    public override void OnLoseFocus()
    {
        
    }
    //Function to let boss open doors
    public void bossOpenDoor()
    {
        //Has master key open all doors
        if (objName.Contains("Door1"))
            objName = "Door1";
        else if (objName.Contains("Door2"))
            objName = "Door2";
        if (!isOpen)
        {
            isOpen = true;
            door.Play("Door2_Open");
            doorAudio.PlayOneShot(doorUse);
        }
        time = 0; //reset timer so door can close if locked
    }

}
