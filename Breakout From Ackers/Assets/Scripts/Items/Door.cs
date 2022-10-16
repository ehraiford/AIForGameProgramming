using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animation door;
    [SerializeField] private AudioSource doorSound;
    private bool isOpen;
    private string objName;
    void Start()
    {
        door = GetComponentInParent<Animation>();
        objName = door.name;
        isOpen = false;
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("DOOR OPEN");
            door.Play(objName+"_Open");
            doorSound.Play();
        }
        else
        {
            isOpen = false;
            Debug.Log("DOOR Close");
            door.Play(objName+"_Close");
            doorSound.Play();
        }
    }


    public override void OnLoseFocus()
    {
        
    }

}
