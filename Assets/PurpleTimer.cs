using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleTimer : MonoBehaviour
{
    public float durationP;
    public Image fillImageP;
    private float alphaLevelP;

    private Transform target;

    private RectTransform rt;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        fillImageP.fillAmount = 1f;
        alphaLevelP = 0f;
        durationP = 11.25f;

        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        fillImageP.color = new Color(0.7924f, 0, 1, alphaLevelP);

        if (GameController.collidedTF == true)
        {
            StartCoroutine(AlphaIncrease());
            StartCoroutine(Timer(durationP));
        }

        if (GameController.circleActive == true)
        {
            rt.anchoredPosition = new Vector3(0, -100, 0);
        }
        else if (GameController.circleActive == false)
        {
            rt.anchoredPosition = new Vector3(0, 0, 0);
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Timer(float duration)
    {
        alphaLevelP = 0f;
        float startTime = Time.time;
        float time = duration;
        float value = 1f;

        while (Time.time - startTime < duration)
        {
            time -= Time.unscaledDeltaTime;
            value = time / duration;
            fillImageP.fillAmount = value;
            yield return null;
        }
        alphaLevelP = 0;
        fillImageP.fillAmount = 1f;
    }

    public IEnumerator AlphaIncrease()
    {
        alphaLevelP = 0f;
        while (alphaLevelP < 1)
        {
            alphaLevelP += 0.7f * Time.unscaledDeltaTime;
            yield return null;
        }
    }
}