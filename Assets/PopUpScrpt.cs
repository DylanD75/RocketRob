using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpScrpt : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float moveYSpeed = 1f;
    public float disappearTimer;
    private Color textColor;
    private float disappearSpeed = 3f;
    public string phrase;

    public bool money;

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    void Update()
    {
        if (money == true)
        {
            phrase = "+$" + Mathf.Round((int)Score.scoreAmount);
        }

        textMesh.text = phrase;

        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.unscaledDeltaTime;

        if(disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.unscaledDeltaTime;
            textMesh.color = textColor;

            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
