using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class AboutGoBack : MonoBehaviour
{
    public void goToGame()
    {
        Advertisement.Banner.Hide();

        SceneManager.LoadScene("Game");
    }
}
