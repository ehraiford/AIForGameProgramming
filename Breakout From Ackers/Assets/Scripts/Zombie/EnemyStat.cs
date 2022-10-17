using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStats
{

    public float damage = 15;
    public float attackSpeed = 3f;
    private BoxCollider[] collsions;
    Transform player;
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController").transform;
        Anim = GetComponentInChildren<Animator>();
        collsions = GetComponentsInChildren<BoxCollider>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void KillCharacter()
    {
        currentHealth = 0;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = player.position - transform.position;

        //Check where player from enemy
        if(Vector3.Dot(forward, toOther) < 0)
        {
            //Player is behind me fall forward
            Anim.SetBool("isDeadBehind", true);
        }
        else
        {
            //Player is infront of me fall backwards
            Anim.SetBool("isDeadInFront", true);

        }
        //Turn of all colliders
        foreach(BoxCollider collsion in collsions)
        {
            collsion.enabled = false;
        }
    }

    protected override void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0) KillCharacter();
    }

    public void DoDamage(float dmg)
    {
        ApplyDamage(dmg);
    }
}
