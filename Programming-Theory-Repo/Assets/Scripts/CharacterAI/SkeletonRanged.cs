using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRanged : SkeletonMain
{
    GameObject projectile;
    private void Awake()
    {
        SetTargetName();
        speed = 10;
        maxHealth = 20;
        minDamage = 5;
        maxDamage = 7;
        prefDist = 10;
        coolDownLength = 2;
        projCoolDown = 0;
        projectile = (GameObject) Resources.Load("Prefabs/Projectile/SkeleArrow");
    }

    protected override void Attack(GameObject target)
    {
        GameObject tempProjectile = Instantiate(projectile, transform.position, transform.rotation);
        tempProjectile.GetComponent<ArrowAI>().GetTargetandDam(targetName, RandomizeAttackDam(), projCoolDown);
    }

}
