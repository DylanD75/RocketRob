using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class StatsCalc : MonoBehaviour
{
    public Text totalStatsText;
    public Button backButton;

    void Start()
    {
        Advertisement.Banner.Hide();

        StatsText.scoreTotal = PlayerPrefs.GetFloat("scoreTotal");
        StatsText.rocketsFiredTotal = PlayerPrefs.GetFloat("rocketsFiredTotal");
        StatsText.enemiesHitTotal = PlayerPrefs.GetFloat("enemiesHitTotal");
        StatsText.meteorsDodgedTotal = PlayerPrefs.GetFloat("meteorsDodgedTotal");
        StatsText.gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        StatsText.hitByMeteor = PlayerPrefs.GetFloat("hitByMeteor");
        StatsText.tripleShotUses = PlayerPrefs.GetFloat("tripleShotUses");
        StatsText.fistShotUses = PlayerPrefs.GetFloat("fistShotUses");
        StatsText.fivePointsGotten = PlayerPrefs.GetFloat("fivePointsGotten");
        HighScore.highScore = PlayerPrefs.GetFloat("highScore");

        StatsText.accuracyTotal = (StatsText.enemiesHitTotal / StatsText.rocketsFiredTotal) * 100;

        totalStatsText.text = "High Score: " + HighScore.highScore.ToString("0") + "\n" + "Total Score: " + StatsText.scoreTotal.ToString("0") + "\n" + "Games Played: " + StatsText.gamesPlayed + "\n" + "Rockets Fired: " + StatsText.rocketsFiredTotal + "\n" + "Enemies Destroyed: " + StatsText.enemiesHitTotal + "\n" + "Accuracy: " + StatsText.accuracyTotal.ToString("0.00") + "%" + "\n" + "Meteors Dodged: " + StatsText.meteorsDodgedTotal + "\n" + "Hit by meteor: " + StatsText.hitByMeteor + "\n" + "Triple Shot uses: " + StatsText.tripleShotUses + "\n" + "Power Fist uses: " + StatsText.fistShotUses + "\n" + "+5 points gotten: " + StatsText.fivePointsGotten;
        backButton.enabled = true;
    }

    public void goToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
