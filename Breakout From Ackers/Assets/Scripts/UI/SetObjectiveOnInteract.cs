using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectiveOnInteract : Interactable
{
    private GameObject onScreenUI;
    private bool alreadyDone;
    [SerializeField] int objectiveNumber;
    [SerializeField] float delayTime;
    private float startTime;
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if (!alreadyDone)
        {
            startTime = Time.time;
            alreadyDone = true;

        }
    }

    public override void OnLoseFocus()
    {
    }

    void Start()
    {
        onScreenUI = GameObject.Find("On Screen UI");
        alreadyDone = false;
        delayTime = 0;
        startTime = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime != -1 && Time.time > startTime + delayTime)
        {
            Debug.Log("Should be set"); 
            startTime = -1;
            onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(objectiveNumber);
        }   
    }
}
