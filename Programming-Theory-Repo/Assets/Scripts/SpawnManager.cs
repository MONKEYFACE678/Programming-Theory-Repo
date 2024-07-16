using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bowSkele;
    public GameObject punchSkele;
    public GameObject meleeEnemy;
    public void spawnBowSkele()
    {
        Instantiate(bowSkele, new Vector3(0, 1, 0), bowSkele.transform.rotation);
    }

    public void spawnPunchSkele()
    {
        Instantiate(punchSkele, new Vector3(0, 1, 0), punchSkele.transform.rotation);
    }

    public void spawnMeleeEnemy()
    {
        Instantiate(meleeEnemy, new Vector3(0, 1.25f, 9.99f), meleeEnemy.transform.rotation);
    }
}
