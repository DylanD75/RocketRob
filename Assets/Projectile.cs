using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;

    public GameObject destroyEffect;

    private float currentSpeed;

    public SpriteRenderer spriteRenderer;
    public Sprite fistSprite;
    public Sprite rocketSprite;

    public TrailRenderer trailRenderer;

    public BoxCollider2D collider;

    private bool holder;

    public void Start()
    {
        holder = true;
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy && !GameController.fistShot && holder)
        {
            transform.localScale = new Vector2(1, 1);
            collider.size = new Vector2(0.25f, 0.18f);
            collider.offset = new Vector2(0, 0);
            trailRenderer.startColor = new Color(0.8773585f, 0.8773585f, 0.8773585f, 0.39215686274f);
            trailRenderer.endColor = new Color(0.8773585f, 0.8773585f, 0.8773585f, 0);
            spriteRenderer.sprite = rocketSprite;
            currentSpeed = GameController.speedOfRocket;
            holder = false;
        }
        
        if (gameObject.activeInHierarchy && GameController.fistShot && holder)
        {
            transform.localScale = new Vector2(2, 2);
            collider.size = new Vector2(0.19f, 0.14f);
            collider.offset = new Vector2(-0.01f, 0.005f);
            trailRenderer.startColor = new Color(1, 0.3490196f, 0, 0.69f);
            trailRenderer.endColor = new Color(1, 0.3490196f, 0, 0);
            spriteRenderer.sprite = fistSprite;
            currentSpeed = GameController.speedOfRocket * 2;
            holder = false;
        }


        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Rocket").GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

        transform.Translate(Vector2.right * Time.deltaTime * (1.4f * currentSpeed + 3.1f));

        if (((transform.position.x < SpawnEnemies.screenBounds.x - 7) || (transform.position.x > SpawnEnemies.screenBounds.x + 1) || (transform.position.y > SpawnEnemies.screenBounds.y + 1) || (transform.position.y < SpawnEnemies.screenBounds.y - 15)))
        {
            holder = true;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!GameController.fistShot)
            {
                Instantiate(destroyEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f), Quaternion.identity);
                if (Settings.sfxOn)
                {
                    FindObjectOfType<GameController>().Play("Rocket Explosion" + Random.Range(1, 3));
                }
                holder = true;
                gameObject.SetActive(false);
            }


            explosion(transform.position);
        }
        else if (other.gameObject.CompareTag("Meteor"))
        {
            explosion(transform.position);
            if (!GameController.fistShot)
            {
                if(Settings.sfxOn)
                {
                    FindObjectOfType<GameController>().Play("Rocket Explosion (Boundary)");
                }
                Instantiate(destroyEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f), Quaternion.identity);
                holder = true;
                gameObject.SetActive(false);
            }
        } else if (other.gameObject.CompareTag("Boundary"))
        {
            explosion(transform.position);
            if (Settings.sfxOn)
            {
                FindObjectOfType<GameController>().Play("Rocket Explosion (Boundary)");
            }
            Instantiate(destroyEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f), Quaternion.identity);
            holder = true;
            gameObject.SetActive(false);
        }
    }

    void explosion(Vector3 explosionPos)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(explosionPos, radius);

        foreach(Collider2D obj in objects)
        {
            Debug.Log("Register");
            if(obj.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Destroyed");
                obj.gameObject.GetComponent<Enemy>().addStuff();
                obj.gameObject.GetComponent<Enemy>().death();
                explosion(obj.gameObject.transform.position);
            }
        }
    }

    private float radius = 0.5f;
}
