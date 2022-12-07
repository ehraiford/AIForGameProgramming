using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSetObjective : MonoBehaviour
{
    [SerializeField] int objectiveNumber = 1;
    private GameObject onScreenUI;
    void Start()
    {
        onScreenUI = GameObject.Find("On Screen UI");

    }

    private void OnTriggerEnter(Collider other)
    {
        onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(objectiveNumber);
        
        Destroy(gameObject);
    }
}
