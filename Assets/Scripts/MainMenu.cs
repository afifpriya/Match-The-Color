using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject circle;
    public GameObject background;
    SpriteRenderer spriteRenderer;
    Wallpaper wallpaper;
    Color currentColor;


    // Start is called before the first frame update
    void Start()
    {
        wallpaper = circle.GetComponent<Wallpaper>();
        spriteRenderer = background.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        wallpaper.Spawn();
        ChangeColor();
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
