using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject bowSkele;
    GameObject punchSkele;
    GameObject meleeEnemy;
    Vector3 skeleSpawn = new Vector3(0, 1, 0);
    Vector3 enemySpawn = new Vector3(0, 1.25f, 9.99f);
    string skeletonPath = "Prefabs/Skeletons/";
    string enemyPath = "Prefabs/Enemies/";

    private void Start()
    {
        bowSkele = (GameObject) Resources.Load(skeletonPath + "RangedSkeleton");
        punchSkele = (GameObject) Resources.Load(skeletonPath + "MeleeSkeleton");
        meleeEnemy = (GameObject) Resources.Load(enemyPath + "MeleeEnemy");
    }

    public void spawnBowSkele()
    {
        Instantiate(bowSkele, skeleSpawn, bowSkele.transform.rotation);
    }

    public void spawnPunchSkele()
    {
        Instantiate(punchSkele, skeleSpawn, punchSkele.transform.rotation);
    }

    public void spawnMeleeEnemy()
    {
        Instantiate(meleeEnemy, enemySpawn, meleeEnemy.transform.rotation);
    }
}
