using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSetObjective : MonoBehaviour
{
    [SerializeField] int objectiveNumber = 1;
    private GameObject onScreenUI;
    [SerializeField] bool firstOne = false;
    void Start()
    {
        onScreenUI = GameObject.Find("On Screen UI");

    }

    private void OnTriggerEnter(Collider other)
    {
        onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(objectiveNumber);
        if (firstOne)
        {
            onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Check inventory screen for updated objective.");
        }
        Destroy(gameObject);
    }
}
