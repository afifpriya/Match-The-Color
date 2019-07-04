using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Color activeColor;
    public Image buttonPanel;
    public GameObject gameOverPanel;
    [Header("Health")]
    public GameObject health1;
    public GameObject health2, health3;
    public Text scoreText;
    public float health = 3;
    public float score;

    Color currentColor;

    [HideInInspector]
    public List<Color> colors = new List<Color>() { Color.red, new Color(1, 1, 0, 1), Color.blue, Color.green };
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
        Health();
        CheckDeath();
    }

    void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            if (hitInfo)
            {
                PowerUpController powerUpController = hitInfo.transform.gameObject.GetComponent<PowerUpController>();
                EnemyController enemyController = hitInfo.transform.gameObject.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    if (enemyController.color == currentColor)
                    {
                        score += enemyController.score;
                        scoreText.text = "Score : " + score.ToString();
                        Destroy(enemyController.gameObject);
                    }
                }
                else if (powerUpController != null)
                {
                    health += powerUpController.healthValue;
                    Destroy(powerUpController.gameObject);
                }
            }
        }
    }

    void Health()
    {
        if (health > 3)
            health = 3;
            switch (health)
            {
                case 3:
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(true);
                    health3.gameObject.SetActive(true);
                    break;
                case 2:
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(true);
                    health3.gameObject.SetActive(false);
                    break;
                case 1:
                    health1.gameObject.SetActive(true);
                    health2.gameObject.SetActive(false);
                    health3.gameObject.SetActive(false);
                    break;
            }
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void ChangeActiveColor(int order)
    {
        activeColor = colors[order];
        buttonPanel.color = Color.Lerp(currentColor, activeColor, 2f);
        currentColor = activeColor;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
