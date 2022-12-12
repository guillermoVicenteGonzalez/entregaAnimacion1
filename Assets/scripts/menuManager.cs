using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public GameObject fadeInPanel;
    public float fadeTime;
    [SerializeField] private string game;
    [SerializeField] private string mainMenu;

    
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
        StartCoroutine(TriggerTransition(game));
    }

    public IEnumerator TriggerTransition(string sceneToLoad)
    {
        Instantiate(fadeInPanel);
        yield return new WaitForSeconds(fadeTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void backToMenu()
    {
        StartCoroutine(TriggerTransition(mainMenu));
    }

    public void loadScene(string newScene)
    {
        StartCoroutine(TriggerTransition(newScene));
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
