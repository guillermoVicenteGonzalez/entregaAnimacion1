using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondo : MonoBehaviour
{

    protected MeshRenderer meshRenderer;
    [SerializeField] private float velocidadFondo = .02f;
    [SerializeField] private float velocidadMedio = .8f;
    [SerializeField] private float velocidadFrente = .1f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.materials[0].SetTextureOffset("_MainTex",new Vector2(0,Time.time * velocidadFondo));
        meshRenderer.materials[1].SetTextureOffset("_MainTex", new Vector2(0, Time.time * velocidadMedio));
        meshRenderer.materials[2].SetTextureOffset("_MainTex", new Vector2(0, Time.time * velocidadFrente));
    }
}
