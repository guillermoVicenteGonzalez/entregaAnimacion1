using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] ParticleSystem particulas;
    // Start is called before the first frame update



    private void Start()
    {
        animator = GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemyBullet"))
        {
            Destroy(collision.gameObject);
        }else if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().enemigoMorir();
        }
    }
    
    public void exitAnimation()
    {
        animator.SetTrigger("exit");
    }

}
