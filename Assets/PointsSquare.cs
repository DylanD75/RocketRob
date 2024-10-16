using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSquare : MonoBehaviour
{
    public GameObject scoreEffect;

    private Transform target;

    private Transform boundary;

    private GameObject scoreHolder;
    private Score currentScore;

    [SerializeField]
    private Transform scoreSquarePopUp;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        GameObject bo = GameObject.FindGameObjectWithTag("Boundary");
        boundary = bo.transform;

        scoreHolder = GameObject.FindGameObjectWithTag("MainCamera");
        currentScore = scoreHolder.GetComponent<Score>();

        if (transform.position.y <= boundary.transform.position.y + 4.8397f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Rocket"))
        {
            if (Settings.sfxOn)
            {
                FindObjectOfType<GameController>().Play("5 Points");
            }
            currentScore.AddScore(5);
            StatsText.fivePointsGotten++;
            Instantiate(scoreSquarePopUp, new Vector3(transform.position.x, transform.position.y, transform.position.z - 9f), Quaternion.identity);
            Invoke("ScoreEffect", 0f);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Invoke("ScoreEffect", 0f);
            Destroy(gameObject);
        }

        if (transform.position.y < SpawnEnemies.screenBounds.y - 20)
        {
            Destroy(gameObject);
        }
    }

    void ScoreEffect()
    {
        Instantiate(scoreEffect, transform.position, Quaternion.identity);
    }
}