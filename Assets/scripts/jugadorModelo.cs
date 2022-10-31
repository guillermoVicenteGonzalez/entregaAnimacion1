using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorModelo 
{
    protected int vida = 100;
    protected int puntuacion = 0;
    protected float velocidad;

    public int pVida
    {
        get { return vida; }
        set { vida = value; }  
    }

    public int pPuntuacion
    {
        get { return puntuacion; }
        set { puntuacion = value; }
    }

    public float pVelocidad
    {
        get { return velocidad; }
        set { velocidad = value; }
    }
}
