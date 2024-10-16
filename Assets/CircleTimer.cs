using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    private float duration;
    public Image fillImage;
    private float alphaLevel;

    public bool fist;
    public bool tShot;

    private Transform target;

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
        if(fist)
        {
            fillImage.color = new Color(1, 0.3490196f, 0, alphaLevel);
        } else
        {
            fillImage.color = new Color(0.7294f, 0, 1, alphaLevel);
        }

        if (GameController.collidedFIST && fist)
        {
            StartCoroutine(AlphaIncrease());
            StartCoroutine(Timer(duration));
        }

        if (GameController.collidedTF && tShot)
        {
            StartCoroutine(AlphaIncrease());
            StartCoroutine(Timer(duration));
        }

        if (!GameController.fistShot && fist)
        {
            alphaLevel = 0;
        }

        if (!GameController.tripleShot && tShot)
        {
            alphaLevel = 0;
        }

        if (GameController.fistShot && GameController.tripleShot && fist)
        {
            fillImage.transform.localPosition = new Vector2(-339, -126);
        } else
        {
            fillImage.transform.localPosition = new Vector2(-339, -26);
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
        GameController.circleActive = false;
        fillImage.fillAmount = 1f;
    }

    public IEnumerator AlphaIncrease()
    {
        alphaLevel = 0f;
        if(fist)
        {
            while (alphaLevel < 1 && GameController.fistShot)
            {
                alphaLevel += 0.7f * Time.unscaledDeltaTime;
                yield return null;
            }
        } else
        {
            while (alphaLevel < 1 && GameController.tripleShot)
            {
                alphaLevel += 0.7f * Time.unscaledDeltaTime;
                yield return null;
            }
        }
    }
}
