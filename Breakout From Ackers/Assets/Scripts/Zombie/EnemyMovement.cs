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
            Chase();
        }    
        else
            Patrol();

    }

    private void rotateToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.Rotate(Vector3.up, rotation.x * Time.deltaTime * 45f);
    }

    private void Patrol()
    {
        anim.SetFloat("Speed", 1f, .3f, Time.deltaTime);
        if (waypoints.Length == 0) return;
        Vector3 distanceLeft = waypoints[currentWaypoint].position - transform.position;
        if (distanceLeft.magnitude > 0) enemyMesh.SetDestination(waypoints[currentWaypoint].position);
        if (distanceLeft.magnitude < 3f)
        {
            ++currentWaypoint;
            if (currentWaypoint >= waypoints.Length) currentWaypoint = 0;

            enemyMesh.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    private void Chase()
    {
        //player out of range
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance > enemyMesh.stoppingDistance)
        {
            anim.SetFloat("Speed", 1f, .3f, Time.deltaTime);
            enemyMesh.SetDestination(player.position);
            rotateToPlayer();
        }
        //player in range   
        if(distance <= enemyMesh.stoppingDistance)
        {
            anim.SetFloat("Speed", 0f, .3f, Time.deltaTime);
            //Attacking
            
            if (Time.time >= timeOfLastAttack + 2f)
            {
                timeOfLastAttack = Time.time;
                attackPlayer();
                Debug.Log("DMG DONE");

            }
        }

    }

    private void attackPlayer()
    {
        anim.SetTrigger("Attack");
        //DO DAMAGE TO PLAYER
    }
}
