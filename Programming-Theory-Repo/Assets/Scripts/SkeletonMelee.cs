using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee : SkeletonMain
{
    private void Awake()
    {
        speed = 5;
        health = 50;
        minDamage = 7;
        maxDamage = 10;
        prefDist = 3;
    }

    
}
