using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 FINITE STATE MACHINE
-it is an artificial inteligence system to store state's data and transition between them, example: animation system.
-A finite state machine that defines all the states they can be in and how they can get from one state to another state.
-determine the states of the character.
-Zombie we determined five states.
1. idle, 2. wander , 3. chase, 4. attack, 5. death. 
 
 
 */

public class ZombieController : MonoBehaviour
{
    Animator anim;
    public GameObject targetPlayer;
    private NavMeshAgent agent;

    enum STATE { IDLE, WANDER, CHASE, ATTACK, DEAD };

    STATE state = STATE.IDLE;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ///anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case STATE.IDLE:

                break;
            case STATE.WANDER:
                break;
            case STATE.CHASE:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEAD:
                break;
            default:
                break;
        }





        //agent.SetDestination(targetPlayer.transform.position);

        //if(agent.remainingDistance > agent.stoppingDistance)
        //{
        //    anim.SetBool("isWalking",true);
        //    anim.SetBool("isAttacking", false);

        //}
        //else
        //{
        //    anim.SetBool("isWalking", false);
        //    anim.SetBool("isAttacking", true);
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    anim.SetBool("isWalking", true);
        //}
        //else
        //    anim.SetBool("isWalking", false);

        //if (Input.GetKey(KeyCode.R))
        //{
        //    anim.SetBool("isRunning", true);
        //}
        //else
        //    anim.SetBool("isRunning", false);

        //if (Input.GetKey(KeyCode.A))
        //{
        //    anim.SetBool("isAttacking", true);
        //}
        //else
        //    anim.SetBool("isAttacking", false);

        //if (Input.GetKey(KeyCode.D))
        //{
        //    anim.SetBool("isDead", true);
        //}

    }

    void TrunOffAnimtriggers()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", false);
    }
}
