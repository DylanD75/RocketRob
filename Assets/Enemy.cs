using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;

    private Transform target;

    private Transform boundary;

    public GameObject deathEffect;
    public GameObject explosionEffect;

    private Vector2 screenBounds;

    private GameObject scoreHolder;
    private Score currentScore;

    public GameController GC;

    [SerializeField]
    private Transform scorePopUp;

    void Start()
    {
        scoreHolder = GameObject.FindGameObjectWithTag("MainCamera");
        currentScore = scoreHolder.GetComponent<Score>();

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        GameObject bo = GameObject.FindGameObjectWithTag("Boundary");
        boundary = bo.transform;

        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        speed = Random.Range(1.25f, 1.85f);

        if(transform.position.y <= boundary.transform.position.y + 4.8397f)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(GameController.start)
        {
            if (target == null)
            {
                Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f), Quaternion.identity);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if (transform.position.y <= boundary.transform.position.y + 4.8397f)
            {
                transform.position = new Vector2(transform.position.x, boundary.transform.position.y + 4.87f);
            }

            if ((transform.position.x < SpawnEnemies.screenBounds.x - 7.25f) || (transform.position.x > SpawnEnemies.screenBounds.x + 1) || (transform.position.y > SpawnEnemies.screenBounds.y + 1) || (transform.position.y < SpawnEnemies.screenBounds.y - 13))
            {
                StartCoroutine(Despawn());
            }
        }
        

    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);

        if ((transform.position.x < SpawnEnemies.screenBounds.x - 7.25f) || (transform.position.x > SpawnEnemies.screenBounds.x + 1) || (transform.position.y > SpawnEnemies.screenBounds.y + 1) || (transform.position.y < SpawnEnemies.screenBounds.y - 15))
        {
            gameObject.SetActive(false);
        }

    }

    public void death()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f), Quaternion.identity);
        if(Settings.sfxOn)
        {
            FindObjectOfType<GameController>().Play("Rocket Explosion (Boundary)");
        }
        gameObject.SetActive(false);
    }

    public void addStuff()
    {
        currentScore.AddScore(1);
        Instantiate(scorePopUp, new Vector3(transform.position.x - 0.025f, transform.position.y, transform.position.z - 9f), Quaternion.identity);
        StatsText.enemiesHit++;
    }
}