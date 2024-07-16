using UnityEngine;

public class EnemyMain : CharacterMain
{
    protected void SetTargetName()
    {
        targetName = "Skeleton";
    }
    protected override void Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
