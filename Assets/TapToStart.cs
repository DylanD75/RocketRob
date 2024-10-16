using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    public static bool tap;

    void Awake()
    {
        tap = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            tap = true;

            if (GameOverScript.musicOff == false)
            {
                FindObjectOfType<GameController>().Play("Song");
            }

            enabled = true;
            Destroy(gameObject);
        }
    }
}
