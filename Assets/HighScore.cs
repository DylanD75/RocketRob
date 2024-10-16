using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;
    public Transform player;

    private float t = 0f;

    private float alphaLevel;

    public static float highScore;

    void Start()
    {
        alphaLevel = 0f;

        highScore = PlayerPrefs.GetFloat("highScore");
    }

    void Update()
    {
        if (highScore < Score.scoreAmount)
        {
            highScore = Score.scoreAmount;
        }

        highScoreText.color = new Color(1, 0.79682f, 0.1764706f, alphaLevel);

        highScoreText.text = "High Score: " + highScore.ToString("0");

        if (player == null)
        {
            StartCoroutine(FadeIn());
        }
    }

    public IEnumerator FadeIn()
    {
        while (t < 1)
        {
            yield return new WaitForSecondsRealtime(2f);
            t += 0.3f * Time.unscaledDeltaTime;
            alphaLevel = Mathf.Lerp(0f, 1f, t);
        }
    }
}
