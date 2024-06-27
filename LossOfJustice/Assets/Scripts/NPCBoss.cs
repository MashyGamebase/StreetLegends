using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBoss : AIAgent
{
    public Stat stats;
    public Transform mainTarget;
    public Transform firePoint;

    // Attacking variables
    public float timeBetweenAttacks = 1f;
    public float attackRange = 5f;
    bool alreadyAttacked;

    // Projectile to Shoot
    public GameObject projectile;

    public float HorizontalForce = 3f;
    public float VerticalForce = 1f;

    public List<Image> hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;

    public void TakeDamage(int amount)
    {
        stats.health -= amount;

        if(stats.health <= 0)
        {
            GotCaught();
        }
    }

    public override void GotCaught()
    {
        // Might be useful to add additional parameters
        GameHandler.instance.SetGameWin();
        base.GotCaught();
    }

    private void Update()
    {
        WanderBehaviour();
        VisualsUpdate();
    }

    public override void WanderBehaviour()
    {
        // Attack if the AI is not moving

        if (!isAgentMoving())
        {
            // Attack
            AttackPattern();
        }

        base.WanderBehaviour();
    }

    public void AttackPattern()
    {
        transform.LookAt(mainTarget);

        if (!alreadyAttacked)
        {
            /// Attack Pattern Code

            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * HorizontalForce, ForceMode.Impulse);
            rb.AddForce(firePoint.up * VerticalForce, ForceMode.Impulse);

            animator.SetTrigger("Attack");

            float distanceToTarget = Vector3.Distance(transform.position, mainTarget.position);

            if (distanceToTarget > attackRange)
            {
                agent.SetDestination(mainTarget.position);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        if(isAgentMoving())
            agent.ResetPath();
        alreadyAttacked = false;
    }

    void VisualsUpdate()
    {
        for(int i = 0; i < hearts.Count; i++)
        {
            if(i < stats.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}



[System.Serializable]
public class Stat
{
    public float health;
    // Other stats that might be useful
    // public float attack;
}