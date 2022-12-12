using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coolDownScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image icono;
    private float proporcion;
    private bool isOnCooldown;
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (icono.fillAmount < 1)
        {
            icono.fillAmount = (Time.time - timer) * proporcion;
        }
    }

    public void iniciarCoolDown(float cooldown)
    {
        icono.fillAmount=0;
        proporcion = 1/cooldown;
        timer = Time.time;
    }
}
