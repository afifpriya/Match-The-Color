using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject circle;
    public GameObject background;
    public GameObject exitPanel;
    public Text exitText;
    SpriteRenderer spriteRenderer;
    Wallpaper wallpaper;
    Color currentColor;

    bool exit = false;
    float delayTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        exitPanel.SetActive(false);
        wallpaper = circle.GetComponent<Wallpaper>();
        spriteRenderer = background.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        wallpaper.Spawn();
        ChangeColor();
        Exit();
    }

    void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !exit)
        {
            exitPanel.SetActive(true);
            StartCoroutine(FadeOut(delayTime));
            exit = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exit)
        {
            Application.Quit();
        }
    }

    IEnumerator FadeOut(float duration)
    {
        float counter = 0;
        Color color = Color.white;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            exitText.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        if (counter >= duration)
        {
            exitPanel.SetActive(false);
            exit = false;
        }
    }

    void ChangeColor()
    {
        GameObject circle = GameObject.FindGameObjectWithTag("Circle");
        if (circle != null)
        {
            Wallpaper temp = circle.GetComponent<Wallpaper>();
            if (temp.change)
            {
                spriteRenderer.color = temp.color;
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayGame");
    }
}
