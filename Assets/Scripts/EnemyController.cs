using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    EnemyManager enemyManager;
    GameManager gameManager;

    float moveSpeed;
    float diagonalMove;
    Rigidbody2D mRigidbody;
    SpriteRenderer spriteRenderer;
    public Color color;
    public float score;
    float gameScore;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = enemyManager.currentEnemy.color;
        color = spriteRenderer.color;
        score = enemyManager.currentEnemy.score;
        mRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = enemyManager.currentEnemy.moveSpeed;
        diagonalMove = enemyManager.currentEnemy.diagonalMove;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        mRigidbody.velocity = transform.right * diagonalMove + -transform.up * moveSpeed;

        if (gameManager.score % 500 == 0 && gameManager.score >= 500)
        {
            float multiplier = gameManager.score / 500;
            moveSpeed = multiplier / 10 * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            gameManager.health--;
            Destroy(gameObject, 1f);
        }
        if (collision.CompareTag("Border"))
        {
            diagonalMove *= -1;
        }
    }
}
