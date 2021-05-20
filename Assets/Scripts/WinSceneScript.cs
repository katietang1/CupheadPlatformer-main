using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneScript : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Goodbye, come again!");
        Application.Quit();
    }
}
