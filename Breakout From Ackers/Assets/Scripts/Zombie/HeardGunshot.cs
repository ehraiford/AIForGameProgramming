using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeardGunshot : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float speed = 1f;
    float time;
    Quaternion lookRotation;
    Vector3 newDir;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Lets try setting the position via scaling it by the normalized vector?

        Vector3 targetPos = new Vector3(player.position.x, animator.transform.position.y, player.position.z);
        //animator.transform.LookAt(targetPos);

        //Determin which direction to rotate towards
        Vector3 targetDir = player.position - agent.transform.position;

        //step size
        float singleStep = speed * Time.deltaTime;
        
        //Rotate the foward Vector
        Vector3 newDir = Vector3.RotateTowards(agent.transform.forward, targetPos, singleStep, 0.0f);

        Debug.DrawRay(agent.transform.position, targetDir, Color.red);
        animator.transform.LookAt(newDir);
        agent.transform.rotation = Quaternion.LookRotation(newDir);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
