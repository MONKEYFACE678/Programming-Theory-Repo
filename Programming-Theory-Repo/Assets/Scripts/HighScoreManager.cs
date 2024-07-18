using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    static int[] highscores = new int[10];
    static string save;
    static string path;
    [SerializeField] TextMeshProUGUI highScoreList;


    private void Start()
    {
        Load();
        highScoreList.text = IntArrayToString(highscores);
    }
    public static void Save()
    {
        path = Application.persistentDataPath + "/SaveData.txt";
        StreamWriter writer = new StreamWriter(path,false);
        Wrapper<int> wrapper = new Wrapper<int>();
        wrapper.array = highscores;
        save = JsonUtility.ToJson(wrapper);
        writer.WriteLine(save);
        writer.Close();
    }

    public static void Load()
    {
        path = Application.persistentDataPath + "/SaveData.txt";
        StreamReader reader = new StreamReader(path, true);
        save = reader.ReadToEnd();
        highscores = JsonUtility.FromJson<Wrapper<int>>(save).array;
        reader.Close();
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public T[] array;
    }

    string IntArrayToString(int[] intArr)
    {
        string retStr ="Highest Waves:\n";
        foreach(int i in intArr)
        {
            retStr += i + "\n";
        }
        return retStr;
    }

    public static void CheckAndAddScore(int i, int placement)
    {
        Load();
        if (placement > 9 || placement < 0)
        {
            Save();
            return;
        }
        if(i >= highscores[placement])
        {
            int temp = highscores[placement];
            highscores[placement] = i;
            Save();
            CheckAndAddScore(temp, placement + 1);
        }
        else
        {
            CheckAndAddScore(i, placement + 1);
        }
    }

    public void BackToTitle()
    {
        Save();
        SceneManager.LoadScene(sceneName: "TitleScreen");
    }
}
