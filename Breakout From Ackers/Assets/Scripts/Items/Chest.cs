using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private Animator anim;
    public bool isLocked = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLocked = true;
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (!isLocked)
        {
            anim.SetBool("isOpen", true);
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    

   
}
