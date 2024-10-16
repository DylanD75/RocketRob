using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    [SerializeField]
    private Transform centerBackground;

    [SerializeField]
    private Transform upperBackground;

    [SerializeField]
    private Transform leftBackground;

    [SerializeField]
    private Transform rightBackground;

    [SerializeField]
    private Transform upperLeftBackground;

    [SerializeField]
    private Transform upperRightBackground;



    void Update()
    {
        if (transform.position.y >= centerBackground.position.y + 17f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y + 17f);
            leftBackground.position = new Vector2(leftBackground.position.x, transform.position.y + 17f);
            rightBackground.position = new Vector2(rightBackground.position.x, transform.position.y + 17f);
        }
        else if (transform.position.y <= centerBackground.position.y - 17f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y - 17f);
            leftBackground.position = new Vector2(leftBackground.position.x, transform.position.y - 17f);
            rightBackground.position = new Vector2(rightBackground.position.x, transform.position.y - 17f);
        }

        if (transform.position.y >= upperBackground.position.y + 17f)
        {
            upperBackground.position = new Vector2(upperBackground.position.x, transform.position.y + 17f);
            upperLeftBackground.position = new Vector2(upperLeftBackground.position.x, transform.position.y + 17f);
            upperRightBackground.position = new Vector2(upperRightBackground.position.x, transform.position.y + 17f);
        }
        else if (transform.position.y <= upperBackground.position.y - 17f)
        {
            upperBackground.position = new Vector2(upperBackground.position.x, transform.position.y - 17f);
            upperLeftBackground.position = new Vector2(upperLeftBackground.position.x, transform.position.y - 17f);
            upperRightBackground.position = new Vector2(upperRightBackground.position.x, transform.position.y - 17f);
        }

        if (transform.position.x >= centerBackground.position.x + 16f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x + 16f, centerBackground.position.y);
            leftBackground.position = new Vector2(leftBackground.position.x + 16f, leftBackground.position.y);
            rightBackground.position = new Vector2(rightBackground.position.x + 16f, rightBackground.position.y);
        }
        else if (transform.position.x <= centerBackground.position.x - 16f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x - 16f, centerBackground.position.y);
            leftBackground.position = new Vector2(leftBackground.position.x - 16f, leftBackground.position.y);
            rightBackground.position = new Vector2(rightBackground.position.x - 16f, rightBackground.position.y);
        }

        if (transform.position.x >= upperBackground.position.x + 16f)
        {
            upperBackground.position = new Vector2(upperBackground.position.x + 16f, upperBackground.position.y);
            upperLeftBackground.position = new Vector2(upperLeftBackground.position.x + 16f, upperLeftBackground.position.y);
            upperRightBackground.position = new Vector2(upperRightBackground.position.x + 16f, upperRightBackground.position.y);
        }
        else if (transform.position.x <= upperBackground.position.x - 16f)
        {
            upperBackground.position = new Vector2(upperBackground.position.x - 16f, upperBackground.position.y);
            upperLeftBackground.position = new Vector2(upperLeftBackground.position.x - 16f, upperLeftBackground.position.y);
            upperRightBackground.position = new Vector2(upperRightBackground.position.x - 16f, upperRightBackground.position.y);
        }
    }
}
