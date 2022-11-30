using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossChasingBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float timerToAttack;
    BossStat bossStat;
    Transform lastKnownPos;
    float time;
    float timeToIdle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Dont think we need this
        /*if (animator.name == "Zombie Mutant")
        {
            bossFootSteps = animator.GetComponent<AudioSource>();
            bossFootSteps.Play();
        }*/
        bossStat = animator.GetComponentInParent<BossStat>();
        timerToAttack = bossStat.attackSpeed;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        time = 0;
        timeToIdle = 0;
        lastKnownPos = null;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        timeToIdle += Time.deltaTime;
        if(timeToIdle > 13)
        {
            animator.SetBool("isChasing", false);
        }
        float distance = Vector3.Distance(animator.transform.position, player.position);
        //Debug.Log(distance);
        //Close enough to attack 
        if (distance <= agent.stoppingDistance)
        {
            if (timerToAttack > 0)
            {
                timerToAttack -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("isAttacking", true);
            }

        }
        else
        {
            timerToAttack = bossStat.attackSpeed;
        }
        /*if (false) //developed blue mass yet?
        {*/
            //Lose sight of player
            if (!agent.GetComponent<FOV>().canSeePlayer)
            {
                //Set last known position
                if (lastKnownPos == null)
                {
                    lastKnownPos = player;
                    //Move to last known pos
                    agent.SetDestination(lastKnownPos.transform.position);
                }

                if (lastKnownPos != null)
                {
                    float distance2 = Vector3.Distance(animator.transform.position, lastKnownPos.position);
                    time += Time.deltaTime;
                    //At last known position switch animation to idle or unable to reach position
                    if (distance2 <= agent.stoppingDistance || time > 8)
                    {
                        animator.SetBool("isChasing", false);
                    }
                }
            }
        //}
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
        animator.SetBool("isChasing", false);
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
