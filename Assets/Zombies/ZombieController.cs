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
    public float walkingSpeed;
    public float runningSpeed;
    

    enum STATE { IDLE, WANDER, CHASE, ATTACK, DEAD };

    STATE state = STATE.IDLE;
   

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        ///anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case STATE.IDLE:
                if (isVissible())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0,500)<5)
                {
                    state = STATE.WANDER;
                }
                else
                {
                    state = STATE.WANDER;
                }
                
                
                break;
            case STATE.WANDER:
                if(!agent.hasPath)
                {
                    float newRandompositionX = this.transform.position.x + Random.Range(-10, 10);
                    float newRandompositionZ = this.transform.position.z + Random.Range(-10, 10);
                    float newRandompositionY = Terrain.activeTerrain.SampleHeight(new Vector3(newRandompositionX, 0, newRandompositionZ));
                    Vector3 finalDestination = new Vector3(newRandompositionX, newRandompositionY, newRandompositionZ);
                    agent.SetDestination(finalDestination);
                    agent.stoppingDistance = 0f;
                    TurnOffAnimtriggers();
                    agent.speed = walkingSpeed;
                    anim.SetBool("isWalking", true);
                    
                    //if (transform.position == finalDestination)
                    //{
                    //    state = STATE.IDLE;
                    //}
                }
                if(isVissible())
                {
                    state=STATE.CHASE;
                }
                else if (Random.Range(0,500)<5)
                {
                    TurnOffAnimtriggers();
                    agent.ResetPath();
                    state = STATE.IDLE;
                }
                
                break;
            case STATE.CHASE:
                agent.SetDestination(targetPlayer.transform.position);
                agent.stoppingDistance = 2.0f;
                TurnOffAnimtriggers();
                agent.speed = runningSpeed;
                anim.SetBool("isRunning",true);
                if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }
                else if (ZombieCanSeeplayer())
                {
                    state = STATE.WANDER;
                    agent.ResetPath();
                }
                break;
            case STATE.ATTACK:
                TurnOffAnimtriggers();
                anim.SetBool("isAttacking", true);
                transform.LookAt(targetPlayer.transform);
                if (DistanceToThePlayer() > agent.stoppingDistance + 2f)
                {
                    state = STATE.CHASE;
                }
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
    bool ZombieCanSeeplayer()
    {
        if (DistanceToThePlayer()>10)
        {
            return true;
        }
        return false;
    }

    void TurnOffAnimtriggers()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", false);
    }

    private float DistanceToThePlayer()
    {
        return Vector3.Distance(targetPlayer.transform.position, this.
            transform.position);
    }
    bool isVissible()
    {
        
        //logic for zombie to see the player and chase
        if (DistanceToThePlayer() < 10f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
