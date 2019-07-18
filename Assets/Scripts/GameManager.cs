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
    public GameObject pausePanel;
    [Header("Health")]
    public GameObject health1;
    public GameObject health2, health3;
    public Text scoreText;
    public Text finalScoreText;
    public Text highScoreText;
    public float health = 3;
    public float score;

    bool pause = false;
    Color currentColor;

    [HideInInspector]
    public List<Color> colors = new List<Color>() { Color.red, new Color(1, 1, 0, 1), Color.blue, Color.green };
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
        Health();
        CheckDeath();
        if (Input.GetKeyDown(KeyCode.Escape) && !pause || Input.GetKeyDown(KeyCode.A) && !pause)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause || Input.GetKeyDown(KeyCode.A) && pause)
        {
            Resume();
        }
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
            finalScoreText.text = scoreText.text;
            if (score > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }
            highScoreText.text = "High Score : " + PlayerPrefs.GetFloat("HighScore", 0).ToString();
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

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        pause = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pause = false;
    }

}
