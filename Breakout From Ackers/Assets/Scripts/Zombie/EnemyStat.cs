using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStats
{

    public float damage = 25;
    public float attackSpeed = 1.2f;
    private BoxCollider[] collsions;
    Transform player;
    Animator Anim;
    string name;
    [SerializeField] private AudioSource groanSound;
    [SerializeField] private AudioSource damagedSound;
    [SerializeField] private AudioSource DeathSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController").transform;
        Anim = GetComponentInChildren<Animator>();
        collsions = GetComponentsInChildren<BoxCollider>();
        currentHealth = maxHealth;
        name = transform.name;
        
        Invoke("ZombieGroan", 0f);
    }

    void ZombieGroan()
    {
        if(currentHealth > 0)
        {
            float randomTime = Random.Range(0f, 10f);

            groanSound.Play();

            Invoke("ZombieGroan", randomTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDistance = player.transform.position - transform.position;
        if (playerDistance.magnitude < 10)
        {
            groanSound.volume = .5f;
        }
        else
        {
            groanSound.volume = 0f;
        }
    }

    protected override void KillCharacter()
    {
        currentHealth = 0;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = player.position - transform.position;

        //Check where player from enemy Normal Zombie
        if(name != "Boss")
        {
            DeathSound.Play();
            if (Vector3.Dot(forward, toOther) < 0)
            {
                //Player is behind me fall forward
                Anim.SetBool("isDeadBehind", true);
            }
            else
            {
                //Player is infront of me fall backwards
                Anim.SetBool("isDeadInFront", true);

            }
        }
        else
        {
            Anim.SetBool("isDead", true);
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
        if (currentHealth <= 0)
        {
            KillCharacter();
        }
        else
        { 
            OnDamage?.Invoke(currentHealth);
            damagedSound.Play();
        }
    }

    public void DoDamage(float dmg)
    {
        ApplyDamage(dmg);
    }
}
