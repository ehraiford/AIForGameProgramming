using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSetFlavorText : MonoBehaviour
{
    [SerializeField] string flavorText;
    private GameObject onScreenUI;
    void Start()
    {
        onScreenUI = GameObject.Find("On Screen UI");

    }

    private void OnTriggerEnter(Collider other)
    {
        onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText(flavorText);

        Destroy(gameObject);
    }
}
