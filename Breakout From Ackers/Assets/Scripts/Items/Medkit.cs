using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float healTime = 1f;

    private GameObject fpsController;

    // Start is called before the first frame update
    void Start()
    {
        fpsController = GameObject.Find("FirstPersonController");
    }

    // Update is called once per frame
    void Update()
    {
        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Heal());
        }
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(healTime);
        fpsController.GetComponent<FirstPersonController>().AddHealth(50);
    }
}
