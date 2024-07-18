using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject bowSkele;
    GameObject punchSkele;
    GameObject meleeEnemy;
    Vector3 skeleSpawn = new Vector3(0, 1, 0);
    Vector3 enemySpawn = new Vector3(0, 1.25f, 9.99f);
    string skeletonPath = "Prefabs/Skeletons/";
    string enemyPath = "Prefabs/Enemies/";
    int waveNumber = 1;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI waveText;
    const string manaTextPre = "Mana: ";
    const string waveTextPre = "Wave: ";
    int mana = 5;
    int bowSkeleCost = -5;
    int punchSkeleCost = -10;


    private void Start()
    {
        bowSkele = (GameObject) Resources.Load(skeletonPath + "RangedSkeleton");
        punchSkele = (GameObject) Resources.Load(skeletonPath + "MeleeSkeleton");
        meleeEnemy = (GameObject) Resources.Load(enemyPath + "MeleeEnemy");
        StartCoroutine(WaveMaking(1));
        SpawnBowSkele();
    }

    private void Update()
    {
        manaText.text = manaTextPre + mana;
        waveText.text = waveTextPre + waveNumber;
        if (GameObject.FindGameObjectWithTag("Skeleton") == null)
        {
            HighScoreManager.CheckAndAddScore(waveNumber, 0);
            SceneManager.LoadScene("HighScoreScene");
        }
    }

    public void SpawnBowSkele()
    {
        if (adjustMana(bowSkeleCost))
        {
            Instantiate(bowSkele, skeleSpawn, bowSkele.transform.rotation);
        }
    }

    public void SpawnPunchSkele()
    {
        if(adjustMana(punchSkeleCost))
        { 
            Instantiate(punchSkele, skeleSpawn, punchSkele.transform.rotation);
        }
    }

    public void SpawnMeleeEnemy()
    {

        Instantiate(meleeEnemy, enemySpawn, meleeEnemy.transform.rotation);
    }

    IEnumerator WaveMaking(int enemyNum)
    {
        for(int i = 0; i < enemyNum; i++)
        {
            SpawnMeleeEnemy();
        }
        while(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            yield return null;
        }
        waveNumber++;
        StartCoroutine(WaveMaking(waveToNumEnemy(waveNumber)));
    }
    
    public bool adjustMana(int mana)
    {
        if (this.mana + mana < 0)
        {
            return false;
        }
        this.mana += mana;
        return true;
    }

    int waveToNumEnemy(int waveNum)
    {
        return waveNum;
    }
}
