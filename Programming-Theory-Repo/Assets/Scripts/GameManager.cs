using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject bowSkele;
    GameObject punchSkele;
    GameObject spraySkele;
    GameObject meleeEnemy;
    GameObject bowEnemy;
    Vector3 skeleSpawn = new Vector3(0, 1, 0);
    Vector3 enemySpawn = new Vector3(0, 1.25f, 9.99f);
    const string skeletonPath = "Prefabs/Skeletons/";
    const string enemyPath = "Prefabs/Enemies/";
    int waveNumber = 1;
    [SerializeField] GameObject meleeButton;
    [SerializeField] GameObject rangedButton;
    [SerializeField] GameObject sprayButton;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI waveText;
    const string manaTextPre = "Mana: ";
    const string waveTextPre = "Wave: ";
    int mana = 5;
    const int bowSkeleCost = -5;
    const int punchSkeleCost = -10;
    const int spraySkeleCost = -25;


    private void Start()
    {
        rangedButton.SetActive(false);
        meleeButton.SetActive(false);
        sprayButton.SetActive(false);
        bowSkele = (GameObject) Resources.Load(skeletonPath + "RangedSkeleton");
        punchSkele = (GameObject) Resources.Load(skeletonPath + "MeleeSkeleton");
        spraySkele = (GameObject) Resources.Load(skeletonPath + "SpraySkeleton");
        meleeEnemy = (GameObject) Resources.Load(enemyPath + "MeleeEnemy");
        bowEnemy = (GameObject)Resources.Load(enemyPath + "RangedEnemy");
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
        if(mana >= 5)
        {
            rangedButton.SetActive(true);
        }

        if(mana >= 10)
        {
            meleeButton.SetActive(true);
        }
        if(mana >= 25)
        {
            sprayButton.SetActive(true);
        }
    }

    public void SpawnBowSkele()
    {
        if (AdjustMana(bowSkeleCost))
        {
            Instantiate(bowSkele, skeleSpawn, bowSkele.transform.rotation);
        }
    }

    public void SpawnPunchSkele()
    {
        if(AdjustMana(punchSkeleCost))
        { 
            Instantiate(punchSkele, skeleSpawn, punchSkele.transform.rotation);
        }
    }

    public void SpawnSpraySkele()
    {
        if (AdjustMana(spraySkeleCost))
        {
            Instantiate(spraySkele,skeleSpawn,spraySkele.transform.rotation);
        }
    }

    public void SpawnMeleeEnemy()
    {

        Instantiate(meleeEnemy, enemySpawn, meleeEnemy.transform.rotation);
    }

    public void SpawnRangedEnemy()
    {
        Instantiate(bowEnemy, enemySpawn, bowEnemy.transform.rotation);
    }

    IEnumerator WaveMaking(int enemyNum)
    {
        for(int i = 0; i < enemyNum; i++)
        {
            SpawnMeleeEnemy();
        }
        for(int i = 4; i < enemyNum; i += 3)
        {
            SpawnRangedEnemy();
        }
        while(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            yield return null;
        }
        waveNumber++;
        StartCoroutine(WaveMaking(WaveToNumEnemy(waveNumber)));
    }
    
    public bool AdjustMana(int mana)
    {
        if (this.mana + mana < 0)
        {
            return false;
        }
        this.mana += mana;
        return true;
    }

    int WaveToNumEnemy(int waveNum)
    {
        float enemyNum = (int) (waveNum / 2 + 0.5);
        enemyNum = Mathf.Pow(enemyNum, 1.5f);
        return (int) enemyNum;
    }
}
