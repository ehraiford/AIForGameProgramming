using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandles : Interactable
{
    [SerializeField] private GameObject flame;
    private bool unlit;
    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(false);
        unlit = true;

    }
    public override void OnFocus()
    {
        print("Looking at " + gameObject.name);
    }

    public override void OnInteract()
    {
        print("Interacted with " + gameObject.name);
        flame.SetActive(true);
        unlit = false;
    }

    public override void OnLoseFocus()
    {
        print("No longer looking at " + gameObject.name);
    }
}
