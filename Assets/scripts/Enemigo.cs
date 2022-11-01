using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Enemigo : MonoBehaviour
{

    public float velocidad = .2f;
    protected Rigidbody2D rb;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    protected EnemigoModelo enemigoModelo;
    private manager mg;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -velocidad);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();
        enemigoModelo = new EnemigoModelo();
        mg = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (vector2min.y > spriteRender.bounds.max.y)
        {
            mg.restarAvionesActuales();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cambiar esto para contemplar todas las habilidades.
        //podria ser tag player bullet por ejemplo
        if (collision.gameObject.name.StartsWith("normalBullet"))
        {
            enemigoModelo.pVida -=1;
            Debug.Log(enemigoModelo.pVida);
            //aqui podriamos iniciar el metodo "explota" y enseñar la animacion
            Destroy(collision.gameObject);
        }

        if(enemigoModelo.pVida <= 0 || collision.gameObject.name.StartsWith("jugador"))
        {
            mg.restarAvionesActuales();
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("explosion", true);
        }
    }

    public void destruirInstancia()
    {
        
        Destroy(gameObject);
    }

    void disparar()
    {

    }
}
