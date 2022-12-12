using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EnemyState
{
    waiting,
    ready
};
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Enemigo : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public EnemyState enemyState = EnemyState.ready;
    public float velocidad = .2f;
    protected Rigidbody2D rb;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    protected EnemigoModelo enemigoModelo;
    private manager mg;
    private Collider2D colisiones;
    public bool dañando = false;
    public ParticleSystem particulas;

    //audio
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip explosion;

    //vida
    public GameObject healthPrefab;


    void Start()
    {
        particulas = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -velocidad);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();
        enemigoModelo = new EnemigoModelo();
        mg = FindObjectOfType<manager>();
        colisiones = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (vector2min.y > spriteRender.bounds.max.y)
        {
            if(mg != null)
                mg.restarAvionesActuales();
            Destroy(this.gameObject);
        }

        if(enemyState == EnemyState.ready)
        {
            disparar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cambiar esto para contemplar todas las habilidades.
        //podria ser tag player bullet por ejemplo
        if (collision.gameObject.name.StartsWith("normalBullet"))
        {          
                enemigoModelo.pVida -=5;
                Debug.Log(enemigoModelo.pVida);
                //aqui podriamos iniciar el metodo "explota" y enseñar la animacion
                Destroy(collision.gameObject);
                particulas.Emit(1);
        }

        if(enemigoModelo.pVida <= 0 || collision.gameObject.name.StartsWith("jugador"))
        {
            colisiones.enabled = false;
            if (!dañando)
            {
                dañando = true;
                enemigoMorir();
           
            }
        }
    }

    public void enemigoMorir()
    {
        leaveCure();
        audioSource.PlayOneShot(explosion);
        if(mg != null)
        {
            mg.restarAvionesActuales();
        }
        Animator animator = GetComponent<Animator>();
        animator.SetBool("explosion", true);
        Jugador jugador;
        jugador = GameObject.Find("jugador").GetComponent<Jugador>();
        jugador.getJugador().pPuntuacion +=5;

        TMP_Text textPuntuacion = GameObject.Find("puntuacion").GetComponent<TMP_Text>();
        textPuntuacion.text = "Puntuacion: " + jugador.getJugador().pPuntuacion;
    }

    private void leaveCure()
    {
        int result = Random.Range(0, 9);
        if(result == 1)
        {
            Instantiate(healthPrefab, trans.position,Quaternion.identity);
        }
    }

    public IEnumerator Cooldown(float waitTime)
    {
        enemyState = EnemyState.waiting;
        yield return new WaitForSeconds(waitTime);
        enemyState = EnemyState.ready;
    }
    public void destruirInstancia()
    {
        Destroy(gameObject);
    }

    void disparar()
    {
        float waitTime = Random.Range(2, 4);
        audioSource.PlayOneShot(shootSound);
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.AngleAxis(180,Vector3.right));
        StartCoroutine(Cooldown(waitTime));
    }
}
