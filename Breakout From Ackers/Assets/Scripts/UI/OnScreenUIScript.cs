using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public  class OnScreenUIScript : MonoBehaviour
{

    public TextMeshProUGUI ammoCount;
    public static bool inventoryUp = false;
    private readonly int amountChanged;
    public GameObject inventoryMenuUI;
    
    [SerializeField] private Color healthIndicatorColor;
    [SerializeField] private Image healthIndicator;
    private int health = 100;

    void Start()
    {
        setOnscreenValue();
    }

    private void setOnscreenValue()
    {
        ammoCount.text = "15 / 40";
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PauseScript.isPaused)
        {
            if (inventoryUp)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

    }

    private void OpenInventory()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        inventoryUp = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseInventory()
    {
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        inventoryUp = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void changeAmmoDisplay()
    {
     //   ammoCount.text =  FirstPersonController.currentAmmo + " / " + FirstPersonController.maxAmmo;
    }

    public void changeHealthIndicator()
    {
        health = health - 10;
        if (health >= 90)
        {
            healthIndicatorColor.r = 0.1768868f;
            healthIndicatorColor.g = 0.5f;
            healthIndicatorColor.b = 0.2438213f;
        }
        else if (health > 60)
        {
            healthIndicatorColor.r = 0.3067373f;
            healthIndicatorColor.g = 0.5754717f;
            healthIndicatorColor.b = 0.414604f;
        }else if (health > 40)
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.4325789f;
            healthIndicatorColor.b = 0.1764706f;
        }
        else if(health > 20)
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.3170498f;
            healthIndicatorColor.b = 0.1764706f;
        }
        else
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.1764706f;
            healthIndicatorColor.b = 0.1793361f;
        }
        healthIndicatorColor.a = 1f;
        healthIndicator.color = healthIndicatorColor;
    }
}
