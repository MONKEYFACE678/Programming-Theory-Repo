using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRanged : SkeletonMain
{
    [SerializeField] GameObject projectile;
    private void Awake()
    {
        SetTargetName();
        speed = 10;
        health = 20;
        minDamage = 5;
        maxDamage = 7;
        prefDist = 10;
        coolDownLength = 2;
    }

    protected override void Attack(GameObject target)
    {
        GameObject tempProjectile = Instantiate(projectile, transform.position, transform.rotation);
        tempProjectile.GetComponent<SkeleRangedProjectileAI>().GetTargetandDam(target, RandomizeAttackDam());
    }

}
