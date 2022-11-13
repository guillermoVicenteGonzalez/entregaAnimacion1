using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public GameObject fadeInPanel;
    public float fadeTime;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        //SceneManager.LoadScene("SampleScene");
        StartCoroutine(TriggerTransition());
    }

    public IEnumerator TriggerTransition()
    {
        Instantiate(fadeInPanel);
        yield return new WaitForSeconds(fadeTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }
}
