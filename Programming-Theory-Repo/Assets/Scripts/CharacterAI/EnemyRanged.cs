using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyMain
{
    GameObject projectile;
    private void Awake()
    {
        SetTargetName();
        speed = 20;
        maxHealth = 5;
        minDamage = 2;
        maxDamage = 5;
        prefDist = 10;
        coolDownLength = 2;
        projCoolDown = 0;
        projectile = (GameObject)Resources.Load("Prefabs/Projectile/EnemyArrow");
    }

    protected override void Attack(GameObject target)
    {
        GameObject tempProjectile = Instantiate(projectile, transform.position, transform.rotation);
        tempProjectile.GetComponent<ArrowAI>().GetTargetandDam(targetName, RandomizeAttackDam(), projCoolDown);
    }

}
