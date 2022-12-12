using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    [SerializeField] private int cureAmount;
    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //tengo en cuanta el borde inferior del sprite
        if (vector2min.y > spriteRenderer.bounds.min.y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Jugador>().changeHealth(cureAmount);
            Destroy(gameObject);
        }
    }
}
