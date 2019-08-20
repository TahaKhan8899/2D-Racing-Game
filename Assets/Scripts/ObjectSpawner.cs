using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject token;

    public GameObject[] debris;
    public GameObject[] enemies;
    public GameObject bigEnemy;

    private float tokenTimer;
    private float enemyTimer;
    private float bigEnemyTimer = 5.0f;

    public int leftSpawnLimit;
    public int rightSpawnLimit;

    public AudioClip bigEnemyFalling;

    // Update is called once per frame
    void Update()
    {
        tokenTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;
        bigEnemyTimer -= Time.deltaTime;
        if (tokenTimer < 0)
        {
            // spawn token
            SpawnToken();
        }
        if (enemyTimer < 0)
        {
            spawnEnemy();
        }
        if (bigEnemyTimer < 0)
        {
            StartCoroutine(spawnBigEnemy());
        }
    }

    void SpawnToken()
    {
        GameObject tok = Instantiate(token, new Vector2(Random.Range(-leftSpawnLimit, rightSpawnLimit), 11), Quaternion.identity) as GameObject;
        tokenTimer = Random.Range(0.5f, 2.5f);
    }
    void spawnEnemy()
    {
        int spawnOnPlayerPercent = (Random.Range(0, 100));
        GameObject badGuy;
        // less than 30% chance of the enemy spawning AWAY from the player
        if (spawnOnPlayerPercent < 30)
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
        GetComponent<AudioSource>().PlayOneShot(bigEnemyFalling, 3.0f);
        bigEnemyTimer = Random.Range(10.0f, 15.0f);
    }
}
