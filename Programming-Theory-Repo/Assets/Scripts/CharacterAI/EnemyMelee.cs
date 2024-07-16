using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyMain
{
    private void Awake()
    {
        SetTargetName();
        speed = 5;
        health = 10;
        minDamage = 4;
        maxDamage = 6;
        prefDist = 3;
    }
    //start and update just so enemy stays still, temp
    override protected void Start()
    {
        base.Start();
        agent.isStopped = true;
    }

    new private void Update()
    {
        
    }
    protected override void Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
