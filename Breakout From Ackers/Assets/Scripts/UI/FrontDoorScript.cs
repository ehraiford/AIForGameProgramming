using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorScript : Interactable
{
    public Animation door;
    private AudioSource doorAudio;
    [SerializeField] private AudioClip doorUse;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioClip doorUnlock;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private FirstPersonController FPC;
    [SerializeField] private string KeyName;
    [SerializeField] GameObject winScreen, backgroundMusic;
    private GameObject onScreenUI;
    public bool isOpen;
    private string objName;
    private float timeStarted = -1;

    void Start()
    {
        FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
        door = GetComponentInParent<Animation>();
        objName = door.name;
        isOpen = false;
        doorAudio = GetComponentInParent<AudioSource>();
    }
    public override void OnFocus()
    {

    }

    private void Update()
    {
        if (timeStarted != -1 && Time.time - timeStarted > 2)
        {
            winScreen.SetActive(true);
            backgroundMusic.GetComponent<BGMScript>().PauseMusic();
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }
    public override void OnInteract()
    {
        if (isLocked)
        {
            //Has key
            if (FPC.RemoveInventoryItem(KeyName, 1) == 1)
            {
                onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Unlocked door using the " + KeyName + ".");
                doorAudio.PlayOneShot(doorUnlock);
                isLocked = false;
            }
            else
            {
                if (KeyName.CompareTo("") == 0)
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
                Debug.Log("DOOR OPEN");
                door.Play("Door2_Open");
                Debug.Log(objName.ToString());
                doorAudio.PlayOneShot(doorUse);
                timeStarted = Time.time;
                
            }
            else
            {
                isOpen = false;
                Debug.Log("DOOR Close");
                door.Play("Door2_Close");
                doorAudio.PlayOneShot(doorUse);
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
            doorAudio.PlayOneShot(doorUse);
        }
    }

}
