using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : CharacterStats
{
    public float damage = 40;
    public float attackSpeed = 3f;
    public bool craftedBlueMass;
    private int timeGotHit;
    private Animator anim;
    float dd;
    // Start is called before the first frame update
    void Start()
    {
        timeGotHit = 1;
        craftedBlueMass = false;
        anim = GetComponentInChildren<Animator>();
        dd = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().diffcultyValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void ApplyDamage(float dmg)
    {

        //Can do damage to the boss
        if (craftedBlueMass)
        {
            currentHealth -= (dmg / dd);
            OnDamage?.Invoke(currentHealth);
            if (currentHealth > 0)
            {
                //Play sound for boss damage
            }
            else
            {
                KillCharacter();
            }

        }
        //Shoot the boss x amout of times to get stunned
        if(timeGotHit > 2)
        {
            //Stun the boss
            anim.SetTrigger("getStunned");
            timeGotHit = 1;

        }
        else //reset counter for stun
        {
            timeGotHit++;
        }
        Debug.Log(timeGotHit);
    }
    public void DoDamage(float dmg)
    {
        //Do damage to zombie
        ApplyDamage(dmg);
        //Make zombie chase player
        anim.SetBool("isChasing", true);
    }
    protected override void KillCharacter()
    {
        anim.SetBool("isDead", true);
        //Play a noise here
    }

    
}
