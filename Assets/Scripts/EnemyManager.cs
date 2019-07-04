using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Enemy[] enemies;
    public PowerUp[] powerUps;
    public Transform[] spawnPoints;

    float spawnDelay = 1f;
    [HideInInspector]
    public PowerUp currentPowerUp;
    [HideInInspector]
    public Enemy currentEnemy;

    GameManager gameManager;
    Transform spawnPoint;
    SpriteRenderer spriteRenderer;
    Color color;
    float score;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Awake()
    {       
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
        InvokeRepeating("SpawnPowerUp", 20f, 20f);
    }

    // Update is called once per frame
    void Update()
    {          

    }

    void SpawnPowerUp()
    {
        currentPowerUp = powerUps[Random.Range(0, powerUps.Length)];
        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(currentPowerUp.sprite, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemy()
    {
        color = gameManager.colors[Random.Range(0, gameManager.colors.Count)];
        currentEnemy = enemies[Random.Range(0, enemies.Length)];
        currentEnemy.color = color;
        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(currentEnemy.sprite, spawnPoint.position, spawnPoint.rotation);
    }
}
