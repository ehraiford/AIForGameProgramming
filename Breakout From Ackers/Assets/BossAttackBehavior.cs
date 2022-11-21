using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossAttackBehavior : StateMachineBehaviour
{

    NavMeshAgent agent;
    GameObject player;
    FirstPersonController playerStat;
    BossStat bossStat;
    float lastTimeOfAttack;
    AudioSource attackSound;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lastTimeOfAttack = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<FirstPersonController>();
        bossStat = animator.GetComponentInParent<BossStat>();
        //attackSound = animator.transform.Find("AttackSound").GetComponent<AudioSource>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Look  at the player
        Transform t = player.transform;
        //Use current object Y so it doesnt rotate
        Vector3 targetPos = new Vector3(t.position.x, animator.transform.position.y, t.position.z);
        animator.transform.LookAt(targetPos);
        //Calculate the distance to see if enemy need to chase again
        float distance = Vector3.Distance(animator.transform.position, player.transform.position);

        if (distance > agent.stoppingDistance)
            animator.SetBool("isAttacking", false);
        //Put time between damage call;
        if (Time.time > lastTimeOfAttack + bossStat.attackSpeed)
        {
            //attackSound.Play();
            lastTimeOfAttack = Time.time;
            playerStat.doDamage(bossStat.damage);
        }
        //Done with attack go back
        animator.SetBool("isAttacking", false);

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
