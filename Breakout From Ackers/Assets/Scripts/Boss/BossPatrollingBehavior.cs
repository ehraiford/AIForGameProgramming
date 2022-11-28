using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
public class BossPatrollingBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPointsFirst = new List<Transform>();
    List<Transform> wayPointsSecond = new List<Transform>();
    NavMeshAgent agent;
    float TimeToIdle; // Duration of patrolling
    Transform player;
    AudioSource bossFootSteps;
    GameObject wpoints1;
    GameObject wpoints2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        TimeToIdle = 30f;

        agent = animator.GetComponentInParent<NavMeshAgent>();
        //agent.SetDestination(wayPoints[0].position);
        //Set the first destination
        wpoints1 = GameObject.Find("BossWpoint1");
        wpoints2 = GameObject.Find("BossWpoint2");
        foreach(Transform item in wpoints1.transform)
        {
            wayPointsFirst.Add(item);
            //Debug.Log(item.localPosition);
        }
        foreach (Transform item in wpoints2.transform)
        {
            wayPointsSecond.Add(item);
            //Debug.Log(item.localPosition);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //agent.SetDestination(wayPointsSecond[9].localPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //If player shoots near zombie, make zombie go to player's location
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (Input.GetButtonDown("Fire1") && distance < 10)
        {
            animator.SetTrigger("heardGunshot");
        }

        //Choose next location to go to
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            
            //Pick between first and second floor
            //based on the player's y position
            //Second Floor
            if (player.transform.position.y > 7f)
            {
                Debug.Log(player.transform.position.y);
                agent.SetDestination(wayPointsSecond[Random.Range(0, wayPointsSecond.Count)].position);
            }
            else if (player.transform.position.y < 6f)//First Floor
            {
                Debug.Log(player.transform.position.y);
                agent.SetDestination(wayPointsFirst[Random.Range(0, wayPointsFirst.Count)].position);
            }
        }

        timer += Time.deltaTime;

        if (timer > TimeToIdle)
        {
            //animator.SetBool("isPatrolling", false);
            //Lets not make the boss idle lets just have him keep roaming around
            //This also prevent him from being soft lock at a locked door
            timer = 0;
            //agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }
        //Chase after players
        if (agent.GetComponent<FOV>().canSeePlayer)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
