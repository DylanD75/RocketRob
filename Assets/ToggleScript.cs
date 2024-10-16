using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        if (GameOverScript.musicOff == true)
        {
            toggle.isOn = false;
        } else if (GameOverScript.musicOff == false)
        {
            toggle.isOn = true;
        }
    }

    void Update()
    {
        Debug.Log(GameOverScript.musicOff);
    }
}
