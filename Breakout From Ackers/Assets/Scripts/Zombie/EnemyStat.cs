using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStats
{

    public float damage = 15;
    public float attackSpeed = 3f;
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    protected override void KillCharacter()
    {
        currentHealth = 0;

        // Add respawn/death screen here
        print("Dead");
    }

    protected override void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0) KillCharacter();
    }
}
