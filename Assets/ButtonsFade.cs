using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFade : MonoBehaviour
{
    public GameObject player;

    private float alphaLevel;

    private float t = 0f;

    private Button deathButton;

    private ColorBlock colors;

    public Text buttonText;

    public float r;

    public float g;

    public float b;

    void Start()
    {
        deathButton = GetComponent<Button>();

        colors = deathButton.colors;

        alphaLevel = 0f;
        deathButton.enabled = false;
    }

    void Update()
    {
        colors.normalColor = new Color(r, g, b, alphaLevel);

        buttonText.color = new Color(0, 0, 0, alphaLevel);

        deathButton.colors = colors;

        if(alphaLevel == 1f)
        {
            deathButton.enabled = true;
        }

        if (player == null)
        {
            StartCoroutine(ChangeOpacity());
        }
    }

    public IEnumerator ChangeOpacity()
    {
        while (t < 1)
        {
            yield return new WaitForSecondsRealtime(2f);
            t += 0.3f * Time.unscaledDeltaTime;
            alphaLevel = Mathf.Lerp(0f, 1f, t);
        }
    }
}