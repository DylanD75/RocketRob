using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float jumpForce;

    public static Rigidbody2D _rigidBody;

    public GameObject explosionEffect;
    public GameObject bloodEffect;
    public GameObject headEffect;
    public GameObject bodyEffect;
    public GameObject armEffect;
    public GameObject legEffect;
    public GameObject rocketEffect;

    public float slowmoTime = 1f;

    public SpriteRenderer player;

    public static Color playerColor = new Color(1, 1, 1, 1);

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D col;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    public float maximumSpeed = 8f;

    private GameObject gameController;
    private GameController GC;

    public Slider slider;

    public static bool hitMeteor;

    private void Start()
    {
        hitMeteor = false;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        GC = gameController.GetComponent<GameController>();

        secondHit = false;

        gameObject.SetActive(true);
        _rigidBody = GetComponent<Rigidbody2D>();

        player = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4), Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boundary"))
        {
            PlayerDeathEffect();
        }

        if (other.gameObject.CompareTag("Meteor"))
        {
            StartCoroutine(DeathAfterMeteor());

            GC.SetSlowMo(0);
            GC.SetMaxSlowMo(0);
            StatsText.hitByMeteor++;
            hitMeteor = true;
        }
    }

    void PlayerDeathEffect()
    {
        GameController.deathAmount++;
        FindObjectOfType<GameController>().Stop("Song");
        if (Settings.sfxOn)
        {
            FindObjectOfType<GameController>().Play("Death Sound");
        }
        Instantiate(rocketEffect, transform.position, Quaternion.identity);
        Instantiate(bloodEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 3.95f), Quaternion.identity);
        Instantiate(headEffect, transform.position, Quaternion.identity);
        Instantiate(bodyEffect, transform.position, Quaternion.identity);
        Instantiate(armEffect, transform.position, Quaternion.identity);
        Instantiate(armEffect, transform.position, Quaternion.identity);
        Instantiate(legEffect, transform.position, Quaternion.identity);
        Instantiate(legEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public IEnumerator DeathAfterMeteor()
    {
        yield return new WaitForSecondsRealtime(10);

            PlayerDeathEffect();
    }

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private bool secondHit;
}