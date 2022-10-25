using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        anim.SetBool("isOpen", true);
    }

    public override void OnLoseFocus()
    {
        
    }

    

   
}
