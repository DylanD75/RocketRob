using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public static bool musicOff;

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeMusic()
    {
        if (musicOff == true)
        {
            musicOff = false;
        } else if (musicOff == false)
        {
            musicOff = true;
        }
    }
}
