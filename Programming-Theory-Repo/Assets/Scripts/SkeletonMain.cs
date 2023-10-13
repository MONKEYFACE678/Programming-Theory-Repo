using UnityEngine;

public class SkeletonMain : CharacterMain
{

    protected override void findTarget()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
        }
    }
}
