using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] protected float maxHealth = 100;
    protected float currentHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;

    [Header("Movement Parameters")]
    [SerializeField] protected float walkSpeed = 3.0f;
    [SerializeField] protected float sprintSpeed = 6.0f;

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0) KillCharacter();
    }

    protected abstract void KillCharacter();
}
