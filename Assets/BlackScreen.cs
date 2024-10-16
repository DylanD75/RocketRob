using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public GameObject player;

    private float t = 0f;

    private float alphaLevelB;

    void Start()
    {
        alphaLevelB = 0f;
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alphaLevelB);

        if (player == null)
        {
            StartCoroutine(ChangeOpacity());
        }
    }

    public IEnumerator ChangeOpacity()
    { 
        while(t < 1)
        {
            yield return new WaitForSecondsRealtime(2f);
            t += 0.3f * Time.unscaledDeltaTime;
            alphaLevelB = Mathf.Lerp(0f, 0.3f, t);
        }
    }
}
