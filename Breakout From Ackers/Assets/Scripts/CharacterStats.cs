using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth;
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
        setCurrHealth(maxHealth);
    }
    private void checkHealth()
    {
        if(currentHealth <= 0)
        {
            KillCharacter();
        }
    }
    public void setCurrHealth(float health)
    {
        currentHealth = health;
        checkHealth();
    }
    public void ApplyDamage(float dmg)
    {
        /*currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0) KillCharacter();*/
        float hpAfterDmg = currentHealth - dmg;
        setCurrHealth(hpAfterDmg);
    }
    
    //TODO: MAKE HEALING FUNCTION or just have it be with the player stats

    protected abstract void KillCharacter();
}
