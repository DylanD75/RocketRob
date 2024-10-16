using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float scale;

    public Rigidbody2D rb;

    void Start()
    {
        scale = Random.Range(3.0f, 5.0f);
        rb.gravityScale = 1;

        if (scale >= 3.75f)
        {
            rb.mass = 15;
            rb.drag = 2.25f;
            rb.velocity = transform.up * -3;
        }
        else if (scale < 3.75f)
        {
            rb.mass = 5;
            rb.drag = 2;
            rb.velocity = transform.up * -4;
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        transform.localScale = new Vector3(scale, scale, 1);
    }

    void Update()
    {
        if (transform.position.y < SpawnEnemies.screenBounds.y - 17)
        {
            if(!Player.hitMeteor)
            {
                StatsText.meteorsDodged++;
            }
            gameObject.SetActive(false);
        }

        if (transform.position.y > SpawnEnemies.screenBounds.y + 0.9f)
        {
            rb.angularVelocity = Random.Range(-100f, 100f);
        }

        if (transform.position.y > SpawnEnemies.screenBounds.y + 4)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().death();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 1;
            if(Settings.sfxOn)
            {
                FindObjectOfType<GameController>().Play("Meteor Hit");
            }
        }
    }
}
