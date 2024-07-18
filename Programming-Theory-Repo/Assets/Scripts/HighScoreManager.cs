using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    static int[] highscores = new int[10];
    string save;
    string path;
    [SerializeField] TextMeshProUGUI highScoreList;


    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            highscores[i] = 0;
        }
        path = Application.persistentDataPath + "/test.txt";
        Debug.Log(path);
    }
    public void Save()
    {
        StreamWriter writer = new StreamWriter(path, true);
        save = JsonUtility.ToJson(highscores);
        writer.WriteLine(save);
        writer.Close();
    }

    public void Load()
    {
        StreamReader reader = new StreamReader(path, true);
        save = reader.ReadToEnd();
        highscores = JsonHelper.getJsonArray<int>(save);
        highScoreList.text = highscores.ToString();
    }
}
