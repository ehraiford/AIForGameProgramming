using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 700f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 250f;

    [Header("Audio")]
    [SerializeField] private AudioSource m1911AudioSource = default;
    [SerializeField] private AudioClip shoot = default;
    [SerializeField] private AudioClip reload = default;

    [Header("Ammo")]
    [SerializeField] private int maxMagAmmo = 10;
    [SerializeField] private int maxReservesAmmo = 30;
    private int currentMagAmmo;
    private int currentReservesAmmo;
    [SerializeField] private float reloadTime = 1f;
    private bool isReloading = false;
    private bool isShooting = false;

    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject playerCamera;
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = playerController.GetComponentInChildren<Animator>();

        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        currentMagAmmo = maxMagAmmo;
        currentReservesAmmo = maxReservesAmmo;
    }

    void OnEnable()
    {
        // Handles the case where you switch weapons while reloading
        isReloading = false;
    }

    void Update()
    {
        if (Time.timeScale > 0.9)
        {
            // Can't use the gun while reloading or mid shot
            if (isReloading) return;

            if (isShooting) return;

            // Check if mag runs out of ammo
            if (currentMagAmmo <= 0)
            {
                StartCoroutine(Reload());

                // Exit update so the player cannot shoot
                return;
            }

            //If you want a different input, change it here
            if (Input.GetButtonDown("Fire1"))
            {
                //Calls animation on the gun that has the relevant animation events that will fire
                //gunAnimator.SetTrigger("Fire");
                StartCoroutine(Shoot());
            }

            //If you want a different input, change it here
            if (Input.GetKeyDown(KeyCode.R) && currentMagAmmo != maxMagAmmo)
            {
                StartCoroutine(Reload());
            }
        }
    }


    //This function creates the bullet behavior
    IEnumerator Shoot()
    {
        gunAnimator.SetBool("Shooting", true);
        playerAnimator.SetBool("Shooting", true);
        isShooting = true;

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        /*
         * Old Bullet
         * 
        // Cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { yield break; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, playerCamera.transform.position, playerCamera.transform.rotation).GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * 1);
        */

        m1911AudioSource.PlayOneShot(shoot);

        // Calculates inaccuracy based on how fast the player is moving
        float inaccuracy = 0.0f;

        if (playerController.GetComponent<FirstPersonController>().getCurrentMovement() == "Sprinting")
            inaccuracy = 0.5f;
        else if (playerController.GetComponent<FirstPersonController>().getCurrentMovement() == "Walking")
            inaccuracy = 0.25f;
        else if (playerController.GetComponent<FirstPersonController>().getCurrentMovement() == "Crouch Walking")
            inaccuracy = 0.1f;

        float inaccuracyX = Random.Range(-inaccuracy, inaccuracy);
        float inaccuracyY = Random.Range(-inaccuracy, inaccuracy);

        // Adjusts the bullet spawn point based on inaccuracy
        Vector3 currentCameraPos = playerCamera.transform.position;
        currentCameraPos.x += inaccuracyX;
        currentCameraPos.y += inaccuracyY;

        RaycastHit hit;
        if(Physics.Raycast(currentCameraPos, playerCamera.transform.forward, out hit, 100))
        {
            // Damages zombie based on where it is shot
            if (hit.collider.CompareTag("Zombie/Head"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(100 - (hit.distance / 3));
            else if (hit.collider.CompareTag("Zombie/Body"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(35 - (hit.distance / 3));
            else if (hit.collider.CompareTag("Zombie/Legs"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(25 - (hit.distance / 3));

            if (hit.collider.CompareTag("Door")) Debug.Log("Door");

            // Spawns a bullet hole if the environment is shot
            if (!(hit.collider.CompareTag("Zombie/Head")) && !(hit.collider.CompareTag("Zombie/Body")) && !(hit.collider.CompareTag("Zombie/Legs")) && !(hit.collider.CompareTag("Door")))
            {
                GameObject newHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
                newHole.transform.LookAt(hit.point + hit.normal);
                Destroy(newHole, 5f);
            }
        }

        // Subtract one from the current ammo
        currentMagAmmo--;

        // Stops the user from queuing another shot
        yield return new WaitForSeconds(0.4f);

        isShooting = false;
        playerAnimator.SetBool("Shooting", false);
        gunAnimator.SetBool("Shooting", false);
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    IEnumerator Reload()
    {
        // Exit reload if no reserves ammo
        if (currentReservesAmmo == 0) yield break;

        gunAnimator.SetBool("Reloading", true);
        playerAnimator.SetBool("Reloading", true);
        isReloading = true;

        m1911AudioSource.PlayOneShot(reload);

        yield return new WaitForSeconds(reloadTime);

        // Reload a full mag
        if(currentReservesAmmo >= maxMagAmmo)
        {
            currentReservesAmmo -= (maxMagAmmo - currentMagAmmo);
            currentMagAmmo = maxMagAmmo;
        }
        else // Reload a partial mag
        {
            int ammoNeeded = maxMagAmmo - currentMagAmmo;

            if(ammoNeeded <= currentReservesAmmo)
            {
                currentReservesAmmo -= ammoNeeded;
                currentMagAmmo = maxMagAmmo;
            }
            else
            {
                currentMagAmmo += currentReservesAmmo;
                currentReservesAmmo = 0;
            }
        }
        
        isReloading = false;
        playerAnimator.SetBool("Reloading", false);
        gunAnimator.SetBool("Reloading", false);
    }

    public int getCurrentMagAmmo()
    {
        return currentMagAmmo;
    }

    public int getCurrentReservesAmmo()
    {
        return currentReservesAmmo;
    }
}
