using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public static float spawnTime;

    public static Vector2 screenBounds;

    public GameObject pointSquare;

    public GameObject slowMoSquare;

    public GameObject tripleFSquare;

    public GameObject fistSquare;

    public GameObject player;

    private Rigidbody2D rb;

    public GameObject meteor;

    private float t = 0f;

    private int random;

    ObjectPooler objectPooler;

    IEnumerator SpawnEnemyR()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
        objectPooler.SpawnFromPool("Enemy", new Vector2(screenBounds.x + 1, Random.Range(screenBounds.y - 12, screenBounds.y - 1)), Quaternion.identity);
    }

    IEnumerator SpawnEnemyL()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
        objectPooler.SpawnFromPool("Enemy", new Vector2(screenBounds.x - 7, Random.Range(screenBounds.y - 12, screenBounds.y - 1)), Quaternion.identity);
    }

    IEnumerator SpawnEnemyUp()
    {
        int randomU = Random.Range(1, 4);

        for (int i = 0; i < randomU; i++)
        {
            objectPooler.SpawnFromPool("Enemy", new Vector2(Random.Range(screenBounds.x - 6, screenBounds.x - 0.5f), screenBounds.y + Random.Range(0.4f, 1f)), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
        }
    }

    private void SpawnEnemyDown()
    {
        objectPooler.SpawnFromPool("Enemy", new Vector2(screenBounds.x - 5, screenBounds.y - 14), Quaternion.identity);
        objectPooler.SpawnFromPool("Enemy", new Vector2(screenBounds.x - 1, screenBounds.y - 14), Quaternion.identity);
        objectPooler.SpawnFromPool("Enemy", new Vector2(screenBounds.x - 3, screenBounds.y - 14), Quaternion.identity);
    }

    IEnumerator SpawnMeteor()
    {
        for (int i = 0; i < Random.Range(1, random); i++)
        {
            objectPooler.SpawnFromPool("Meteor", new Vector2(Random.Range(screenBounds.x - 8.5f, screenBounds.x + 2.5f), screenBounds.y + Random.Range(1.75f, 2.5f)), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        }
    }

    private void SpawnPointSquare()
    {
        GameObject a = Instantiate(pointSquare) as GameObject;
        a.transform.position = new Vector2(Random.Range(screenBounds.x - 6, screenBounds.x - 0.5f), screenBounds.y + 1);
    }

    private void SpawnFistSquare()
    {
        GameObject a = Instantiate(fistSquare) as GameObject;
        a.transform.position = new Vector2(Random.Range(screenBounds.x - 6, screenBounds.x - 0.5f), screenBounds.y + 1);
    }

    private void SpawnTripleFSquare()
    {
        GameObject a = Instantiate(tripleFSquare) as GameObject;
        a.transform.position = new Vector2(Random.Range(screenBounds.x - 6, screenBounds.x - 0.5f), screenBounds.y + 1);
    }

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        random = 1;

        spawnTime = 2;

        Time.timeScale = 0f;
        
        StartCoroutine(timeSpawn());

        StartCoroutine(enemyWave());

        StartCoroutine(pointSquareWave());

        StartCoroutine(meteorWave());
        
        StartCoroutine(tripleFSquareWave());

        StartCoroutine(fistSquareWave());

        rb = player.GetComponent<Rigidbody2D>();

        downSpawn = true;
    }

    private bool downSpawn;

    void Update()
    {
        if ((Player.hitMeteor|| GameController.sliderTime == 0) && downSpawn)
        {
            Debug.Log("started");
            StartCoroutine(enemyWaveDown());
            downSpawn = false;
        }

        if (player == null)
        {
            Time.timeScale = 0.6f;
            t += 0.2f * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(0.6f, 0f, t);
        }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    IEnumerator timeSpawn()
    {
        while (spawnTime > 1f)
        {
            yield return new WaitForSeconds(3.75f);
            spawnTime = spawnTime - 0.25f;
            random++;

        }
    }

    IEnumerator enemyWave()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(spawnTime);
            StartCoroutine(SpawnEnemyUp());
            StartCoroutine(SpawnEnemyL());
            StartCoroutine(SpawnEnemyR());

        }
    }

    IEnumerator enemyWaveDown()
    {
        while (player != null)
        {
            Debug.Log("down");
            SpawnEnemyDown();
            yield return new WaitForSeconds(0.75f);
        }
    }

    IEnumerator pointSquareWave()
    {
        while (player != null)
        {
            int randomTime = Random.Range(10, 20);
            yield return new WaitForSeconds(randomTime);
            SpawnPointSquare();
        }
    }

    IEnumerator fistSquareWave()
    {
        while (player != null)
        {
            int randomTime3 = Random.Range(15, 25);
            yield return new WaitForSeconds(randomTime3);
            SpawnFistSquare();

        }
    }

    IEnumerator meteorWave()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(Random.Range(spawnTime - 0.75f, spawnTime));
            StartCoroutine(SpawnMeteor());
        }
    }

    IEnumerator tripleFSquareWave()
    {
        while (player != null)
        {
            int randomTime2 = Random.Range(15, 25);
            yield return new WaitForSeconds(randomTime2);
            SpawnTripleFSquare();

        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}