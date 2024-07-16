using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class CharacterMain : MonoBehaviour, IDamageable
{
    bool onCooldown;
    [SerializeField] protected float health;
    protected int coolDownLength;
    protected string targetName;
    protected int minDamage;
    protected int maxDamage;
    protected int speed;
    protected bool closeToTarget;
    protected bool farToTarget;
    //only protected temp remember to private
    protected NavMeshAgent agent;
    [SerializeField] protected GameObject target;
    protected float distanceFromTarget;
    protected float prefDist;
    public bool isDead;
    private Vector3 dirToTarget;

    public void AdjustHealth(int damage)
    {
        health += damage;
        Debug.Log(damage + "damage taken");
    }

    //make private and nonvirtual when not temp
    protected virtual void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.autoBraking = false;
        agent.stoppingDistance = 0;
        findTarget();
    }

    virtual protected void Update()
    {
        if(health <=0)
        {
            Die();
        }
        if (findTarget())
        {
            distanceFromTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);

            if (distanceFromTarget <= prefDist - 2)
            {
                closeToTarget = true;
            }
            else
            {
                closeToTarget = false;
            }

            if (distanceFromTarget >= prefDist + 2)
            {
                farToTarget = true;
            }
            else
            {
                farToTarget = false;
            }

            if (farToTarget)
            {
                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else if (closeToTarget)
            {
                dirToTarget = transform.position - target.transform.position;
                agent.destination = transform.position + dirToTarget;
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
            if (!onCooldown)
            {
                StartCoroutine(CooldownWithAttack());
            }
            RotateTowardsTarget();

            if (target.GetComponent<CharacterMain>().isDead)
            {
                Destroy(target);
                target = null;
            }
        }
    }


    protected void Damage()
    {
        target.GetComponent<IDamageable>().AdjustHealth(-RandomizeAttackDam());
    }


    private bool findTarget()
    {
        if (target)
        {
            return true;
        }
        if (GameObject.FindGameObjectWithTag(targetName) != null)
        {
            target = GameObject.FindGameObjectWithTag(targetName);
            return true;
        }
        return false;
    }

    protected int RandomizeAttackDam()
    {
        int dam = Random.Range(minDamage, maxDamage + 1);
        return dam;
    }

    protected abstract void Attack(GameObject target);

    protected IEnumerator CooldownWithAttack()
    {
        onCooldown = true; // Set the cooldown flag
        yield return new WaitForSeconds(coolDownLength);
        if (target)
        {
            Attack(target);
        }
        onCooldown = false; // Reset the cooldown flag
    }

    protected void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDir);
            transform.rotation = rotation;
        }
    }

    protected void Die()
    {
        isDead = true;
    }
}
