using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTransition : MonoBehaviour
{
    public GameObject fadeOut;
    // Start is called before the first frame update
    private void Awake()
    {
        if(fadeOut != null)
        {
            GameObject transition = Instantiate(fadeOut);
            Destroy(transition,1.01f);
        }
        else
        {
            Debug.Log("Transición sin asignar");
        }
    }
}
