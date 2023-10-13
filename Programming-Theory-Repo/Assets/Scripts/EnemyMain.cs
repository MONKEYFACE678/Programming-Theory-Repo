using UnityEngine;

public class EnemyMain : CharacterMain
{
    protected override void findTarget()
    {
        if (target == null)
        {
           target = GameObject.FindGameObjectWithTag("Skeleton");
        }
    }
}
