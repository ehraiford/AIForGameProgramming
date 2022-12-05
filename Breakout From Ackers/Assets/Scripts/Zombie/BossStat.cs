using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStat : CharacterStats
{
    public float damage = 40;
    public float attackSpeed = 3f;
    public bool craftedBlueMass;
    private int timeGotHit;
    private Animator anim;
    float dd;
    GameObject onScreenUI;
    public bool isDead;
    public Collider[] cols;
    NavMeshAgent agent;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        timeGotHit = 1;
        craftedBlueMass = false;
        isDead = false;
        anim = GetComponentInChildren<Animator>();
        dd = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().diffcultyValue();
        onScreenUI = GameObject.Find("On Screen UI");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void createdBM()
    {
        craftedBlueMass = true;
        //Buff up the boss
        agent.speed = 4;
        attackSpeed = 1f;
        damage = 60f;
        //Play sound to show the boss is angry
        audioSource.PlayOneShot(audioClips[0]);

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
        isDead = true;
        //Turn off hitbox colliders
        foreach(Collider x in cols)
        {
            x.enabled = false;
        }
        //Turn on collider for getting key
        cols[cols.Length-1].enabled = true;
        onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(8);
        onScreenUI.GetComponent<OnScreenUIScript>().SetHeadsUpText("Retrieve the key from Acker's body and escape.");
    }

    
}
