using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TFCircleTimer : MonoBehaviour
{
    public float duration;
    public Image fillImage;
    private float alphaLevel;

    private Transform target;

    private RectTransform RT;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        fillImage.fillAmount = 1f;
        alphaLevel = 0f;
        duration = 11.25f;
    }

    void Update()
    {
        fillImage.color = new Color(0.7294f, 0, 1, alphaLevel);

        if (GameController.collidedTF == true)
        {
            StartCoroutine(AlphaIncrease());
            StartCoroutine(Timer(duration));
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Timer(float duration)
    {
        float startTime = Time.time;
        float time = duration;
        float value = 1f;

        while (Time.time - startTime < duration)
        {
            time -= Time.unscaledDeltaTime;
            value = time / duration;
            fillImage.fillAmount = value;
            yield return null;
        }
        alphaLevel = 0f;
        fillImage.fillAmount = 1f;
    }

    public IEnumerator AlphaIncrease()
    {
        alphaLevel = 0f;
        while (alphaLevel < 1)
        {
            alphaLevel += 0.9f * Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
