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
        if (Input.GetKeyDown(KeyCode.E))
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
        Cursor.lockState = CursorLockMode.Confined;
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


}
