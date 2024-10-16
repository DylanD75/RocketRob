using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Settings : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle sfxToggle;

    public static bool musicOn;
    public static bool sfxOn;

    void Start()
    {
        Advertisement.Banner.Hide();

        if (musicOn)
        {
            musicToggle.isOn = true;
        }
        else
        {
            musicToggle.isOn = false;
        }

        if (sfxOn)
        {
            sfxToggle.isOn = true;
        }
        else
        {
            sfxToggle.isOn = false;
        }

        if (GameController.deathAmount == 0)
        {
            GameController.deathAmount = 1;
        }
    }

    void Update()
    {
        if(musicToggle.isOn)
        {
            musicOn = true;
        } else
        {
            musicOn = false;
        }

        if (sfxToggle.isOn)
        {
            sfxOn = true;
        }
        else
        {
            sfxOn = false;
        }
    }

    public void goToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
