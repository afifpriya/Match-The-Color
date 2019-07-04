using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallpaper : MonoBehaviour
{
    public Color color;
    SpriteRenderer spriteRenderer;
    Vector2 touchPos;
    public bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        Expand();
    }

    public void Spawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            touchPos = Camera.main.ScreenToWorldPoint(pos);
            Instantiate(gameObject, touchPos, Quaternion.identity);
        }
    }

    void Expand()
    {
        Vector3 temp = transform.localScale;
        if (transform.localScale.x < 30)
        {
            temp += Vector3.one;
            transform.localScale = temp;
        }
        else
        {
            change = true;
            Destroy(gameObject,0.1f);
        }
    }
}
