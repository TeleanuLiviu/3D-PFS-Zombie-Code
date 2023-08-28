using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathSceneButtons : MonoBehaviour
{
    public void QuitButton()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
