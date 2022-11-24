using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject itemHandler;
    private Animator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float attackSpeed = 0.5f;
    public bool isAttacking = false;

    [Header("Audio")]
    [SerializeField] private AudioSource knifeAudioSource = default;
    [SerializeField] private AudioClip slashAir = default;
    [SerializeField] private AudioClip slashObject = default;
    [SerializeField] private AudioClip slashEnemy = default;

    void Start()
    {
        playerAnimator = playerController.GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        // Handles the case where you switch items while attacking
        isAttacking = false;
    }

    void Update()
    {
        if (Time.timeScale > 0.9)
        {
            // Can't use the knife while attacking
            if (isAttacking) return;

            // Attack
            if (Input.GetButtonDown("Fire1") && !itemHandler.GetComponent<ItemSwitching>().isSwitching) StartCoroutine(Attack());
        }
    }


    //This function creates the bullet behavior
    private IEnumerator Attack()
    {
        isAttacking = true;
        playerAnimator.SetBool("Meleeing", true);

        // Randomizes the animation
        float attackVal = Random.Range(0f, 1f);
        playerAnimator.SetFloat("Attack Value", attackVal);

        // Wait for knife to get into slashing position
        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;
        if (Physics.SphereCast(playerCamera.transform.position, 0.75f, playerCamera.transform.forward, out hit, 0.75f))
        {
            // Damages zombie based on where it is sliced
            if (hit.collider.CompareTag("Zombie/Head"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(25);
            else if (hit.collider.CompareTag("Zombie/Body"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(25);
            else if (hit.collider.CompareTag("Zombie/Legs"))
                hit.transform.gameObject.GetComponent<EnemyStat>().DoDamage(25);

            // Add potential melee puzzle

            // Plays audio depending on what is hit
            if (hit.collider.CompareTag("Zombie/Head") || hit.collider.CompareTag("Zombie/Body") || hit.collider.CompareTag("Zombie/Legs"))
            {
                knifeAudioSource.PlayOneShot(slashEnemy);
            }
            else if (hit.collider.CompareTag("Furniture") || hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Floors"))
            {
                knifeAudioSource.PlayOneShot(slashObject);
            }
            else
            {
                knifeAudioSource.PlayOneShot(slashAir);
            }
        }
        else
        {
            knifeAudioSource.PlayOneShot(slashAir);
        }

        // Stops the user from queuing another melee attack
        yield return new WaitForSeconds(attackSpeed);
        playerAnimator.SetBool("Meleeing", false);

        //yield return new WaitForSeconds(attackSpeed - 0.3f);
        isAttacking = false;
    }
}
