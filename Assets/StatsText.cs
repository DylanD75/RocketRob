using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsText : MonoBehaviour
{
    public GameObject player;

    private float t = 0f;

    private float alphaLevel;

    public Text statsText;

    public static float rocketsFired;
    public static float enemiesHit;
    public static float meteorsDodged;
    private float accuracy;

    public static float rocketsFiredTotal;
    public static float enemiesHitTotal;
    public static float meteorsDodgedTotal;
    public static float accuracyTotal;

    public static float scoreTotal;
    public static int gamesPlayed;
    public static float hitByMeteor;
    public static float tripleShotUses;
    public static float fistShotUses;
    public static float fivePointsGotten;


    private bool ready;

    void Start()
    {
        scoreTotal = PlayerPrefs.GetFloat("scoreTotal");
        rocketsFiredTotal = PlayerPrefs.GetFloat("rocketsFiredTotal");
        enemiesHitTotal = PlayerPrefs.GetFloat("enemiesHitTotal");
        meteorsDodgedTotal = PlayerPrefs.GetFloat("meteorsDodgedTotal");
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        hitByMeteor = PlayerPrefs.GetFloat("hitByMeteor");
        tripleShotUses = PlayerPrefs.GetFloat("tripleShotUses");
        fistShotUses = PlayerPrefs.GetFloat("fistShotUses");
        fivePointsGotten = PlayerPrefs.GetFloat("fivePointsGotten");
        HighScore.highScore = PlayerPrefs.GetFloat("highScore");

        alphaLevel = 0f;
        rocketsFired = 0;
        enemiesHit = 0;
        meteorsDodged = 0;

        ready = true;
    }

    void Update()
    {
        statsText.color = new Color(1, 1, 1, alphaLevel);

        if(player != null)
        {
            accuracy = (enemiesHit / rocketsFired) * 100;
            statsText.text = "Rockets Fired: " + rocketsFired + "\n" + "Enemies Destroyed: " + enemiesHit + "\n" + "Accuracy: " + accuracy.ToString("0.00") + "%" + "\n" + "Meteors dodged: " + meteorsDodged;
        }

        if (player == null)
        {
            StartCoroutine(FadeIn());

            if(ready)
            {
                StartCoroutine(calcStats());
                ready = false;
            }
        }
    }

    public IEnumerator FadeIn()
    {
        while (t < 1)
        {
            yield return new WaitForSecondsRealtime(2);
            t += 0.3f * Time.unscaledDeltaTime;
            alphaLevel = Mathf.Lerp(0f, 1f, t);
        }
    }

    public IEnumerator calcStats()
    {
        yield return new WaitForSecondsRealtime(1);
        gamesPlayed++;
        scoreTotal = scoreTotal + Score.scoreAmount;
        rocketsFiredTotal = rocketsFiredTotal + rocketsFired;
        enemiesHitTotal = enemiesHitTotal + enemiesHit;
        meteorsDodgedTotal = meteorsDodgedTotal + meteorsDodged;

        PlayerPrefs.SetFloat("scoreTotal", scoreTotal);
        PlayerPrefs.SetFloat("rocketsFiredTotal", rocketsFiredTotal);
        PlayerPrefs.SetFloat("enemiesHitTotal", enemiesHitTotal);
        PlayerPrefs.SetFloat("meteorsDodgedTotal", meteorsDodgedTotal);
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        PlayerPrefs.SetFloat("hitByMeteor", hitByMeteor);
        PlayerPrefs.SetFloat("tripleShotUses", tripleShotUses);
        PlayerPrefs.SetFloat("fistShotUses", fistShotUses);
        PlayerPrefs.SetFloat("fivePointsGotten", fivePointsGotten);
        PlayerPrefs.SetFloat("highScore", HighScore.highScore);
    }
}