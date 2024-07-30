using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpray : SkeletonMain
{
    GameObject projectile;
    private void Awake()
    {
        SetTargetName();
        speed = 7;
        maxHealth = 25;
        minDamage = 4;
        maxDamage = 8;
        prefDist = 4;
        coolDownLength = 4;
        projCoolDown = 0.5f;
        projectile = (GameObject)Resources.Load("Prefabs/Projectile/SkeleSpray");
    }

    protected override void Attack(GameObject target)
    {
        GameObject tempProjectile = Instantiate(projectile, transform);
        tempProjectile.GetComponent<SprayAI>().GetTargetandDam(targetName, RandomizeAttackDam(), projCoolDown);
    }
}
