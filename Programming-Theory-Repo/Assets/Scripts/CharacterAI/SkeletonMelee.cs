using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee : SkeletonMain
{
    private void Awake()
    {
        SetTargetName();
        speed = 5;
        maxHealth = 50;
        minDamage = 7;
        maxDamage = 10;
        prefDist = 3;
        coolDownLength = 3;
    }

    protected override void Attack(GameObject target)
    {
        Damage();
    }
}
