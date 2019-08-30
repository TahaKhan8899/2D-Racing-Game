using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject token;
    public GameObject diamond;
    public GameObject cam;
    public GameObject[] debris;
    public GameObject[] enemies;
    public GameObject bigEnemy;
    private float tokenTimer;
    private float diamondTimer;
    private float enemyTimer;
    private float bigEnemyTimer = 5.0f;
    private float gapEnemyTimer = 1.0f;
    public int leftSpawnLimit;
    public int rightSpawnLimit;
    public AudioClip bigEnemyFalling;

    // Update is called once per frame
    void Update()
    {
        if (Game_Init.gameStarted == true)
        {
            // If the game just started, use the delayed spawn function
            if (Game_Init.firstFrame)
            {
                StartCoroutine(spawnWait());
            }
            else
            {
                mainStuff();
            }

        }
    }

    // The delayed spawn function
    IEnumerator spawnWait()
    {
        yield return new WaitForSeconds(5);
        mainStuff();
        Game_Init.firstFrame = false;
    }

    void mainStuff()
    {
        tokenTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;
        diamondTimer -= Time.deltaTime;
        bigEnemyTimer -= Time.deltaTime;
        gapEnemyTimer -= Time.deltaTime;
        if (tokenTimer < 0)
        {
            // spawn token
            SpawnToken();
        }
        if (diamondTimer < 0)
        {
            // spawn token
            SpawnDiamond();
        }
        if (enemyTimer < 0)
        {
            spawnEnemy();
        }
        if (bigEnemyTimer < 0)
        {
            StartCoroutine(spawnBigEnemy());
        }
        if (gapEnemyTimer < 0)
        {
            StartCoroutine(spawnGapEnemies());
        }
    }

    void SpawnDiamond()
    {
        GameObject tok = Instantiate(diamond, new Vector2(Random.Range(-leftSpawnLimit, rightSpawnLimit), 11), Quaternion.identity) as GameObject;
        diamondTimer = Random.Range(3.5f, 6.5f);
    }

    void SpawnToken()
    {
        GameObject tok = Instantiate(token, new Vector2(Random.Range(-leftSpawnLimit, rightSpawnLimit), 11), Quaternion.identity) as GameObject;
        tokenTimer = Random.Range(0.5f, 1.5f);
    }
    void spawnEnemy()
    {
        int spawnOnPlayerPercent = (Random.Range(0, 100));
        GameObject badGuy;
        // less than 40% chance of the enemy spawning AWAY from the player
        if (spawnOnPlayerPercent < 40)
        {
            badGuy = Instantiate(enemies[(Random.Range(0, enemies.Length))], new Vector2(Random.Range(-leftSpawnLimit, rightSpawnLimit), 11), Quaternion.identity) as GameObject;
        }
        else
        {
            // spawn above the player
            badGuy = Instantiate(enemies[(Random.Range(0, enemies.Length))], new Vector2(player.transform.position.x, 11), Quaternion.identity) as GameObject;
        }
        enemyTimer = Random.Range(0.5f, 0.9f);

        // Assign a random gravity to this 
        badGuy.GetComponent<Rigidbody2D>().gravityScale = Random.Range(2, 4);
    }

    IEnumerator spawnBigEnemy()
    {
        bigEnemyTimer = 1000;
        Vector2 spawnSpot;
        int debrisTimer = (Random.Range(20, 30));
        int spawnOnPlayerPercent = (Random.Range(0, 100));

        // 50% chance of the big enemy spawning right above the player
        if (spawnOnPlayerPercent < 50)
        {
            spawnSpot = new Vector2(Random.Range(-leftSpawnLimit, rightSpawnLimit), 15);
        }
        else
        {
            // spawn above the player
            spawnSpot = new Vector2(player.transform.position.x, 15);
        }

        while (debrisTimer > 0)
        {
            debrisTimer--;
            float t = Random.Range(0.005f, 0.1f);
            yield return new WaitForSeconds(t);
            Debug.Log("Spawned");

            // instantiate a debris object in a random spot in the array, at spawnspot x, and -10 y b/c debris is lower then the big enemy, and quaternion is rotation
            GameObject debriss = Instantiate(debris[(Random.Range(0, debris.Length))], new Vector2(spawnSpot.x + Random.Range(-1.0f, 1.0f), spawnSpot.y), Quaternion.identity) as GameObject;
        }
        Debug.Log("Complete");
        // yield return new WaitForSeconds(1f);
        Instantiate(bigEnemy, spawnSpot, Quaternion.identity);
        iTween.ShakePosition(cam, new Vector3(0.06f, 0.06f, 0.06f), 2);
        GetComponent<AudioSource>().PlayOneShot(bigEnemyFalling, 3.0f);
        bigEnemyTimer = Random.Range(10.0f, 15.0f);
    }

    IEnumerator spawnGapEnemies()
    {
        // change the timer for the other things so they don't spawn
        bigEnemyTimer = 1000;
        enemyTimer = 1000;
        tokenTimer = 1000;
        gapEnemyTimer = 1000;

        // set the number of spawns to random
        int numEnemySets = Random.Range(1, 5);

        for (int i = 0; i < numEnemySets; i++)
        {
            yield return new WaitForSeconds(1);
            int xpos = -8;
            int gapXPos = Random.Range(-7, 7);
            // TODO audio stuff

            for (int j = 0; j < 20; j++)
            {
                GameObject enemy = Instantiate(enemies[(Random.Range(0, enemies.Length))], new Vector2(xpos, 10), Quaternion.identity) as GameObject;
                // TODO more audio
                enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
                if (xpos != gapXPos)
                {
                    xpos++;
                }
                else
                {
                    xpos = xpos + 3;
                }
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}
