using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneScript : MonoBehaviour
{
    public void ReplayGame()
    {
        Debug.Log("Playing game");
        SceneManager.LoadScene("MainScene");
    }

    public void QuittingGame()
    {
        Debug.Log("Quitting...goodbye");
        Application.Quit();
    }
}
