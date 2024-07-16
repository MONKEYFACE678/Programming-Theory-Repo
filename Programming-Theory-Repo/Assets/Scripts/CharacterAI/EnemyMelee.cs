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
        coolDownLength = 3;
    }
    protected override void Attack(GameObject target)
    {
        Damage();
    }
}
