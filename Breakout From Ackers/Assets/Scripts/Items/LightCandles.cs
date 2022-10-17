using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandles : Interactable
{
    [SerializeField] private GameObject flame;
    private bool unlit;
    AudioSource light;
    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(false);
        unlit = true;
        light = gameObject.GetComponentInChildren<AudioSource>();
    }
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        //Interact with items
        print("Interacted with " + gameObject.name);
        light.Play();
        flame.SetActive(true);
        unlit = false;
    }

    public override void OnLoseFocus()
    {
    }
}
