using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Playing game");
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...goodbye");
        Application.Quit();
    }
}
