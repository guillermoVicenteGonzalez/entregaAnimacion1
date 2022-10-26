using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Jugador : MonoBehaviour
{
    [SerializeField] private float velocidad = .7f;
    private Transform trans;
    protected Animator anim;
    [SerializeField] private GameObject normalBulletPrefab;

    void Start()
    {
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Vector2 vector2 = new Vector2();
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        //para que en diagonal no sume los dos inputs del vector
        vector2.Normalize();

        if(Input.GetAxisRaw("Fire1") == 1)
        {
            anim.SetBool("disparar", true);
            disparar(normalBulletPrefab);
        }
        else
        {
            anim.SetBool("disparar", false);
        }

        mover(vector2);

    }

    void mover(Vector2 vector2)
    {   
        //calculamos los limites
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //aplicamos los inputs y los multiplicamos por la velocidad
        vector2.x *= velocidad * Time.fixedDeltaTime;
        vector2.y *= velocidad * Time.fixedDeltaTime;

        //aplicamos los limites
        vector2.x=Mathf.Clamp(trans.position.x + vector2.x, vector2min.x, vector2max.x);
        vector2.y=Mathf.Clamp(trans.position.y + vector2.y, vector2min.y, vector2max.y);

        //movemos la nave
        trans.position = new Vector2(vector2.x, vector2.y);
    }

    //lo pongo asi para no disparar solo la bala normal y añadir mas balas mas adelante
    void disparar(GameObject prefab)
    {
        Instantiate(prefab, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
        Instantiate(prefab, new Vector2(trans.GetChild(1).position.x, trans.GetChild(1).position.y), Quaternion.identity);
    }
}
