using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoDisplay = default;
    private int currentMagAmmo;
    private int currentReservesAmmo;

    private string currentItemName;
    private GameObject pistol;
    private GameObject medkit;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch items
        pistol = GameObject.Find("M1911");
        medkit = GameObject.Find("MedKit");
    }

    // Update is called once per frame
    void Update()
    {
        if(!pistol)
        {
            pistol = GameObject.Find("M1911");
        }
        if(!medkit)
        {
            medkit = GameObject.Find("MedKit");
        }

        // Fetch current item name
        currentItemName = GameObject.Find("Item Holder").GetComponentInChildren<ItemSwitching>().getCurrentItemName();

        if (currentItemName == "M1911") // Current item is a gun
        {
            // Fetch ammo info
            currentMagAmmo = pistol.GetComponent<Gun>().getCurrentMagAmmo();
            currentReservesAmmo = pistol.GetComponent<Gun>().getCurrentReservesAmmo();

            // Update ammo info
            ammoDisplay.text = currentMagAmmo + " / " + currentReservesAmmo;
        }
        else // Current item is not a gun
        {
            ammoDisplay.text = "";
        }
    }
}
