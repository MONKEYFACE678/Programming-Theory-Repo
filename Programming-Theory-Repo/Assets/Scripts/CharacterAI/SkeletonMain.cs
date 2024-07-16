using UnityEngine;

public class SkeletonMain : CharacterMain
{

    protected void SetTargetName() 
    {
        targetName = "Enemy";
    }
    protected override void Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
