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
        light = gameObject.GetComponent<AudioSource>();
    }
    public override void OnFocus()
    {
        print("Looking at " + gameObject.name);
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
        print("No longer looking at " + gameObject.name);
    }
}
