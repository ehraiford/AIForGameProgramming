using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public  class OnScreenUIScript : MonoBehaviour
{

    public TextMeshProUGUI ammoCount;
    private readonly int amountChanged;
    //public GameObject playerInfo;
    void Start()
    {
        setOnscreenValue();
    }

    private void setOnscreenValue()
    {
        ammoCount.text = "15 / 40";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeAmmoDisplay()
    {
     //   ammoCount.text =  FirstPersonController.currentAmmo + " / " + FirstPersonController.maxAmmo;
    }


}
