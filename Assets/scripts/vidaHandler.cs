using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidaHandler : MonoBehaviour
{

    public Slider slider;
    public Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        //animator = GetComponent<Animator>();
        slider = GetComponent<Slider>();
    }
    public void setVida(int v)
    {
        slider.value = v;
        animator.SetTrigger("golpe");
    }

    public void setMaxVida(int v)
    {
        slider.maxValue = v;
    }
}
