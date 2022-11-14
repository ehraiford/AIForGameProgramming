using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float healTime = 1f;

    [SerializeField] private FirstPersonController playerController;

    void Start()
    {
        
    }

    void Update()
    {
        // If you want a different input, change it here
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Heal());
        }
    }

    private IEnumerator Heal()
    {
        yield return new WaitForSeconds(healTime);
        playerController.AddHealth(50);
    }
}