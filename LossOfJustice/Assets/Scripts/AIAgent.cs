using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    [HideInInspector] public float timer;

    /* Not Used anymore (only used in testing)
    public virtual void Move(Vector3 target)
    {
        agent.SetDestination(target);
    }
    */

    public virtual void WanderBehaviour()
    {
        timer += Time.deltaTime;

        if(timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        AnimationControl();
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitCircle * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public virtual void AnimationControl()
    {
        animator.SetBool("isWalking", isAgentMoving());
    }

    public virtual void GotCaught()
    {
        // Put the animation here but let's destroy them instantly first.
        Destroy(gameObject);
    }


    public bool isAgentMoving()
    {
        return agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude > 0.1f;
    }
}