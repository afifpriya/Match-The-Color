using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    GameManager gameManager;
    Image image;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.activeColor == image.color)
        {
            anim.SetBool("IsThisButton", true);
        }
        else
            anim.SetBool("IsThisButton", false);
    }
}
