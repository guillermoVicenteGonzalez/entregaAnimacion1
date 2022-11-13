using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class normalBullet : MonoBehaviour
{
    public float speed;
    protected Rigidbody2D rigid;
    protected Transform trans;
    protected SpriteRenderer spriteRenderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, speed);
        trans = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //tengo en cuanta el borde inferior del sprite
        if (vector2max.y < spriteRenderer.bounds.min.y)
            Destroy(this.gameObject);
    }
}

