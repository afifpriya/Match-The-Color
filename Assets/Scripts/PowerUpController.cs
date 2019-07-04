using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    EnemyManager enemyManager;
    GameManager gameManager;

    float moveSpeed;
    float diagonalMove;
    [HideInInspector]
    public float healthValue;
    Rigidbody2D mRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        mRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = enemyManager.currentPowerUp.moveSpeed;
        diagonalMove = enemyManager.currentPowerUp.diagonalMove;
        healthValue = enemyManager.currentPowerUp.healthValue;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        mRigidbody.velocity = transform.right * diagonalMove + -transform.up * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            Destroy(gameObject, 1f);
        }

        if (collision.CompareTag("Border"))
        {
            diagonalMove *= -1;
        }
    }
}
