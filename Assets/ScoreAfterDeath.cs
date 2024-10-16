using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAfterDeath : MonoBehaviour
{
    public Text finalScoreText;
    public Transform player;

    private float t = 0f;

    private float alphaLevel;

    private float finalScore;

    void Start()
    {
        alphaLevel = 0f;
    }

    void Update()
    {
        finalScoreText.color = new Color(1, 1, 1, alphaLevel);

        finalScoreText.text = Score.scoreAmount.ToString("0");

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
