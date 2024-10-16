using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleFSquare : MonoBehaviour
{
    public GameObject tripleFEffect;

    private Transform target;

    private Transform boundary;

    public Player player;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        GameObject bo = GameObject.FindGameObjectWithTag("Boundary");
        boundary = bo.transform;

        GameController.collidedTF = false;

        if (transform.position.y <= boundary.transform.position.y + 4.8397f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Rocket"))
        {
            GameController.collidedTF = true;
            if (Settings.sfxOn)
            {
                FindObjectOfType<GameController>().Play("Triple Power Up");
            }
            StatsText.tripleShotUses++;
            Invoke("TripleFEffect", 0f);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Invoke("TripleFEffect", 0f);
            Destroy(gameObject);
        }

        if (transform.position.y < SpawnEnemies.screenBounds.y - 20)
        {
            Destroy(gameObject);
        }
    }

    void TripleFEffect()
    {
        Instantiate(tripleFEffect, transform.position, Quaternion.identity);
    }
}
