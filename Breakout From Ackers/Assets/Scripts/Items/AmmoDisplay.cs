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
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject medkit;
    [SerializeField] private GameObject itemHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Fetch current item name
        currentItemName = itemHolder.GetComponentInChildren<ItemSwitching>().getCurrentItemName();

        if (currentItemName == "M1911") // Current item is a gun
        {
            // Fetch ammo info
            currentMagAmmo = pistol.GetComponent<Gun>().getCurrentMagAmmo();
            currentReservesAmmo = pistol.GetComponent<Gun>().getCurrentReservesAmmo();

            // Update ammo info
            ammoDisplay.text = currentMagAmmo + "/" + currentReservesAmmo;
        }
        else // Current item is not a gun
        {
            ammoDisplay.text = "";
        }
    }
}
