using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(sceneName: "MainScene");
    }

    public void HighScores()
    {
        SceneManager.LoadScene(sceneName: "HighScoreScene");
    }
}
