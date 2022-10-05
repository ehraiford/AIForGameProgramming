using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyMesh;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform player;
    private Animator anim;
    public GameObject enemyObject;
    private int currentWaypoint;
    private float timeOfLastAttack;


    private void Awake()
    {
        enemyMesh = GetComponent<NavMeshAgent>();
        //enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        currentWaypoint = 0;
        anim = GetComponentInChildren<Animator>();
        timeOfLastAttack = 0f;
    }
    void Update()
    {
        if (enemyObject.GetComponent<FOV>().canSeePlayer)
        {
            
        }    
        else
        {

        } 

    }

    private void rotateToPlayer()
    {

    }

    private void Patrol()
    {

    }
    private void Chase()
    {

    }

    private void attackPlayer()
    {
        anim.SetTrigger("Attack");
        //DO DAMAGE TO PLAYER
    }
}
