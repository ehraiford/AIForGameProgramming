using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
public class PatrolBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;

    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        //GameObject parent = animator.transform.parent.parent.gameObject;
        //Just defaulting waypoints then change later
        Transform waypointsObj = GameObject.Find("Waypoints").transform;
        //Debug.Log(parent.name);
        //Room one
        /*
        switch (parent.name)
        {
            case "Foyer Art":
                waypointsObj = GameObject.Find("Waypoints").transform;
                break;
            case "Room2":
                waypointsObj = GameObject.Find("Room2Waypoints").transform;
                break;
            default:
                break;
        }
        */
        
        //Room two
        
        foreach( Transform item in waypointsObj)
        {
            wayPoints.Add(item);
        }
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.SetDestination(wayPoints[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }

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
