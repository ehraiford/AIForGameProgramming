using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        // Set this to the Interactable layer number
        gameObject.layer = 11;
    }

    public abstract void OnInteract(); // Called when the interaction key is pressed
    public abstract void OnFocus(); // Called when the player is looking at the interactable object
    public abstract void OnLoseFocus(); // Called when the player is no longer looking at the interactable object
}
