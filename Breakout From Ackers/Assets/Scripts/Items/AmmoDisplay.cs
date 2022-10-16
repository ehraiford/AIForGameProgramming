using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoDisplay = default;
    private GameObject gun;
    private int currentMagAmmo;
    private int currentReservesAmmo;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("M1911 Handgun_Model");
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch ammo info
        currentMagAmmo = gun.GetComponent<Gun>().getCurrentMagAmmo();
        currentReservesAmmo = gun.GetComponent<Gun>().getCurrentReservesAmmo();

        // Update ammo info
        ammoDisplay.text = currentMagAmmo + "/" + currentReservesAmmo;
    }
}
