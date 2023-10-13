using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRanged : SkeletonMain
{
    private void Awake()
    {
        speed = 10;
        health = 20;
        minDamage = 10;
        maxDamage = 12;
        prefDist = 10;
    }

    protected override void Update()
    {
        base.Update();
    }
}
