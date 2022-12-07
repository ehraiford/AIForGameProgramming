using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
public class BossPatrollingBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
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
        if(wayPoints.Count == 0)
        {
            foreach (Transform item in wpoints1.transform)
            {
                Debug.Log(item.localPosition);
                wayPoints.Add(item);
            }
            foreach (Transform item in wpoints2.transform)
            {
                Debug.Log(item.localPosition);
                wayPoints.Add(item);
            }
        }
        

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //agent.SetDestination(wayPointsSecond[9].localPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //This is to make it so the player must fight acker after blue mass
        if (animator.GetComponentInParent<BossStat>().craftedBlueMass)
           animator.SetBool("isChasing", true);

        //If player shoots near zombie, make zombie go to player's location
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (Input.GetButtonDown("Fire1") && distance < 10)
        {
            animator.SetTrigger("heardGunshot");
        }

        //Pick between first and second floor
        //based on the player's y position
        //Second Floor

        int idx = 0;
        

        //Choose next location to go to
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    if (player.transform.position.y >= 7f)
                    {
                        idx = Random.Range(8, wayPoints.Count - 1); // the rest are second floor

                    }
                    else
                    {
                        idx = Random.Range(0, 7); // the boss has 8 points in the first floor
                    }
                    Debug.Log(idx);
                    agent.SetDestination(wayPoints[idx].position);
                }
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
        animator.SetBool("isPatrolling", false);
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
