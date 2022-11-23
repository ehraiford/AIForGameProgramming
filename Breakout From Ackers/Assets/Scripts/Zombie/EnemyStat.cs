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
    bool isGroaning = false;
    [SerializeField] private AudioClip groanSound;
    [SerializeField] private AudioClip damagedSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource zombieAudio;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController").transform;
        Anim = GetComponentInChildren<Animator>();
        collsions = GetComponentsInChildren<BoxCollider>();
        zombieAudio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        name = transform.name;
    }

    IEnumerator ZombieGroan()
    {
        isGroaning = true;

        if (currentHealth > 0)
        {
            float groanRandom = Random.Range(0f, 1f);

            if (groanRandom < 0.2) zombieAudio.PlayOneShot(groanSound);
        }

        yield return new WaitForSeconds(5f);

        isGroaning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGroaning) StartCoroutine(ZombieGroan());
    }

    protected override void KillCharacter()
    {
        currentHealth = 0;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = player.position - transform.position;

        //Check where player from enemy Normal Zombie
        if(name != "Boss")
        {
            zombieAudio.PlayOneShot(deathSound);
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
        OnDamage?.Invoke(currentHealth);
        if (currentHealth > 0)
        {
            zombieAudio.PlayOneShot(damagedSound);
        }
        else
        {
            KillCharacter();
        }
    }

    public void DoDamage(float dmg)
    {
        //Do damage to zombie
        ApplyDamage(dmg);
        //Make zombie chase player
        Anim.SetBool("isChasing", true);
    }
}
