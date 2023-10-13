using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMain : MonoBehaviour
{
    protected float health;
    protected int minDamage;
    protected int maxDamage;
    protected int speed;
    [SerializeField] protected bool closeToTarget;
    [SerializeField] protected bool farToTarget;
    NavMeshAgent agent;
    protected GameObject target;
    [SerializeField] protected float distanceFromTarget;
    protected float prefDist;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = prefDist;
    }

    virtual protected void Update()
    {
        findTarget();
        if(farToTarget)
        {
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
        else if(closeToTarget)
        {
            agent.isStopped = false;
            agent.destination = transform.position - target.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
        distanceFromTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (distanceFromTarget <= prefDist -2)
        {
            closeToTarget = true;
        }
        else
        {
            closeToTarget = false;
        }

        if(distanceFromTarget >= prefDist +2)
        {
            farToTarget = true;
        }
        else
        {
            farToTarget = false;
        }
    }

    virtual protected void findTarget()
    { 
    }
}
