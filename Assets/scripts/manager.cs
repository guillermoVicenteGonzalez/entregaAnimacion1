using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class manager : MonoBehaviour
{
    public GameObject gameObjectEnemigo;
    private IEnumerator corutina;
    [SerializeField] float maxTotalAviones;
    private float nAvionesActual=0;
    // Use this for initialization
    void Start()
    {
        corutina = coroutinaAvisiones(2);
        StartCoroutine(corutina);
        Debug.Log("Creo aviones " + Time.time);
    }

    private void Update()
    {
        Debug.Log("aviones actuales: " + nAvionesActual);
    }
    private IEnumerator coroutinaAvisiones(float waitTime)
    {
        //The bottom-left of the viewport is (0,0); the topright is (1,1).
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        int aviones = (int)(Random.value * 5);
        float rangox = vector2max.x - vector2min.x;
        float rangoy = (vector2max.y-vector2min.y)*2;
        Vector2 vector2 = new Vector2();
        int nAvionesMaximo = 1;
        while (true)
        {
            if (nAvionesActual <= maxTotalAviones)
            {
                aviones = (int)(Random.value * nAvionesMaximo) + 1;
                nAvionesMaximo+=2;
                for (int i = 0; i < aviones; i++)
                {
                    GameObject gameObjectAvion = Instantiate(gameObjectEnemigo);
                    vector2.x = vector2min.x + rangox * Random.value;
                    vector2.y = vector2max.y + rangoy * Random.value;
                    gameObjectAvion.transform.position = vector2;
                    nAvionesActual++;
                }
            }
            Debug.Log(waitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void restarAvionesActuales()
    {
        nAvionesActual--;
    }

    
}
