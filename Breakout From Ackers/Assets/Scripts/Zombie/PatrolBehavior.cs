using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
public class PatrolBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    float TimeToIdle; // Duration of patrolling
    Transform player;
    AudioSource bossFootSteps;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        if (animator.name == "Zombie Mutant")
        {
            TimeToIdle = 30f;
            bossFootSteps = animator.GetComponent<AudioSource>();
            bossFootSteps.Play();
        }
        else
            TimeToIdle = 10f;
        //Get the parent object (aka the room they are in)
        GameObject parent = animator.transform.parent.parent.parent.gameObject;
        //Debug.Log(parent.name);
        Transform CorrectWaypoints = parent.transform.GetChild(0).transform;
        //Just defaulting waypoints then change later
        
        
        //Add the waypoints to the list
        foreach( Transform item in CorrectWaypoints)
        {
            wayPoints.Add(item);
        }
        agent = animator.GetComponentInParent<NavMeshAgent>();
        //agent.SetDestination(wayPoints[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //If player shoots near zombie, make zombie go to player's location
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (Input.GetButtonDown("Fire1") && distance < 5)
        {
            animator.SetTrigger("heardGunshot");
        }

        //Choose next location to go to
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        timer += Time.deltaTime;
        //Go back to idle
        if (timer > TimeToIdle)
        {
            animator.SetBool("isPatrolling", false);
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
        agent.SetDestination(agent.transform.position);
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
