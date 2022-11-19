using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSetFlavorText : Interactable
{

    [SerializeField] string headsupText;
    private GameObject onScreenUI;
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText(headsupText);
    }

    public override void OnLoseFocus()
    {
    }

    void Start()
    {
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
    }
}
