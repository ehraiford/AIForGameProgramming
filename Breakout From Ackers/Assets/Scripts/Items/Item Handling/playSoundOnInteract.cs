using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundOnInteract : Interactable
{
    [SerializeField] AudioSource audioSource;
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        audioSource.Play();
    }

    public override void OnLoseFocus()
    {
    }
}
