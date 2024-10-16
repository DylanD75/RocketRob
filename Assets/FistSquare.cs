using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistSquare : MonoBehaviour
{
    public GameObject fistExplosion;

    private Transform target;

    private Transform boundary;

    public Player player;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        GameObject bo = GameObject.FindGameObjectWithTag("Boundary");
        boundary = bo.transform;

        if (transform.position.y <= boundary.transform.position.y + 4.8397f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Rocket"))
        {
            GameController.collidedFIST = true;
            if (Settings.sfxOn)
            {
                FindObjectOfType<GameController>().Play("Fist Power Up");
            }
            StatsText.fistShotUses++;
            Invoke("FistEffect", 0f);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Invoke("FistEffect", 0f);
            Destroy(gameObject);
        }

        if (transform.position.y < SpawnEnemies.screenBounds.y - 20)
        {
            Destroy(gameObject);
        }
    }

    void FistEffect()
    {
        Instantiate(fistExplosion, transform.position, Quaternion.identity);
    }
}
