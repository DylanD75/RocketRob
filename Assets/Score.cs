using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public static float scoreAmount;
    private float pointIncreasePerSecond;

    public Text altitudeText;
    public Transform player;
    public Transform boundary;

    private float alphaLevel;

    private RectTransform altitudeRT;

    public static float playerHeight;

    public static float playerAltitude;

    public static float heightMax;

    public static float extraScore;

    void Start()
    {

        heightMax = (((player.position.y - boundary.position.y) - 5) * 5); ;
        playerAltitude = -19.51275f;
        alphaLevel = 0;
        extraScore = 0;
        scoreAmount = 0;
    }

    void Update()
    {
        if (player != null)
        {
            playerHeight = (((player.position.y - boundary.position.y) - 5) * 5);

            if (heightMax < playerHeight)
            {
                heightMax = playerHeight;
            }

            if (playerHeight < 0)
            {
                playerHeight = 0;
            }

            scoreAmount = heightMax + extraScore;
            altitudeText.text = scoreAmount.ToString("0");
        }

    }

    public void AddScore(float amount)
    {
        extraScore += amount;
    }
}
