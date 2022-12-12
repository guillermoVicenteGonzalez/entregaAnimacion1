using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fasesBoss
{
    bigBala,
    spawnMinions,
    smallBala,
};
public class bossScript : MonoBehaviour
{
    public fasesBoss estadoBoss;
    Vector2 vector2min;
    Vector2 vector2max;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 direccion;
    private ParticleSystem particleSystem;
    [SerializeField] private Transform shootPoint;

    //prefabs
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bigBullet;
    [SerializeField] private GameObject smallBullet;

    //tiempos
    [SerializeField] private float tiempoFase;
    [SerializeField] private float tiempoCooldown;

    //vida
    [SerializeField] private int maxVida;
    private EnemigoModelo vida;
    [SerializeField] private vidaHandler barraVida;

    private bool calculating = false;

    [SerializeField] private GameObject win;

    private void Start()
    {
        vida = new EnemigoModelo();
        vida.pVida = maxVida;
        vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        rb = GetComponent<Rigidbody2D>();
        direccion = new Vector2(1, 0);
        barraVida.setMaxVida(vida.pVida);
        barraVida.setVida(vida.pVida);
        particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(nextEnemyState());
    }

    private void FixedUpdate()
    {
        if(vida.pVida <= 0)
        {
            win.SetActive(true);
            Destroy(gameObject);
        }
        mover();
    }

    private void mover()
    {
        if (this.transform.position.x >= vector2max.x)
        {
            direccion.x = -1;
        }
        if (this.transform.position.x <= vector2min.x)
        {
            direccion.x = 1;
        }

        rb.MovePosition((Vector2)transform.position + direccion * Time.fixedDeltaTime * speed);
    }

    private IEnumerator nextEnemyState()
    {
        while(vida.pVida > 0)
        {
            int nuevoEstado;
            yield return new WaitForSeconds(tiempoCooldown);
            nuevoEstado = Random.Range(0, 2);
            estadoBoss = (fasesBoss)nuevoEstado;
            //Debug.Log(estadoBoss);

            if(estadoBoss == fasesBoss.bigBala)
            {
                bigBalaFunc();
            }else if(estadoBoss == fasesBoss.spawnMinions)
            {
                spawnMinionsFunc();
            }else if(estadoBoss == fasesBoss.smallBala)
            {
                StartCoroutine(smallBalaFunc());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            //particulas
            particleSystem.Emit(2);
            Destroy(collision.gameObject);
            vida.pVida -= 5;
            Debug.Log(vida.pVida);
            //actualizar barra de vida
            barraVida.setVida(vida.pVida);
        }
    }

    void bigBalaFunc()
    {
        Debug.Log("big bala");
        Instantiate(bigBullet,shootPoint.position, Quaternion.AngleAxis(180, Vector3.right));
    }

    void spawnMinionsFunc()
    {
        for(int i = 0; i<7; i++)
        {
            Debug.Log("spawn minions");
            float randomNumber = Random.Range(vector2min.x, vector2max.x);
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).y;
            Vector2 vector2 = new Vector2(randomNumber, maxY);
            Instantiate(enemyPrefab, vector2, Quaternion.identity);
        }
    }

    private IEnumerator smallBalaFunc()
    {
        Debug.Log("small bala");
        for(int i = 0; i<10; i++)
        {
            Instantiate(smallBullet, shootPoint.position, Quaternion.AngleAxis(180, Vector3.right));
            yield return new WaitForSeconds(0.2f);

        }
    }



}
