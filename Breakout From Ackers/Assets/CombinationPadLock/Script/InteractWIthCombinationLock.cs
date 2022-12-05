using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractWIthCombinationLock : Interactable
{
    [SerializeField] GameObject firstPersonControllerCamera;
    [SerializeField] GameObject combinationLockCamera;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with Combination Lock");
        firstPersonControllerCamera.SetActive(false);
        combinationLockCamera.SetActive(true);
        gameObject.GetComponent<MoveRuller>().isActive = true;

    }

    public override void OnLoseFocus()
    {

    }

}