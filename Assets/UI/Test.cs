using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public GameObject Loading;
    public Image LoadingScreen;
    public float transitionDuration = 0.3f;
    
    //離開節點
    public void Out()
    {
        //加Map裡的遊戲進度計數
        UIManagerM.GameState++;
        StartCoroutine(LoadOutAndSwitchScene("Map"));
    }

    private IEnumerator LoadIn()
    {
        yield return StartCoroutine(LoadIn());
        Loading.SetActive(false);
    }

    private IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(LoadingQuit());
        yield return new WaitUntil(() => LoadingScreen.fillAmount == 1);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadingEnter()
    {
        Loading.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            LoadingScreen.fillAmount = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LoadingScreen.fillAmount = 0;
    }

    public IEnumerator LoadingQuit()
    {
        Loading.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            LoadingScreen.fillOrigin = 1;
            LoadingScreen.fillAmount = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LoadingScreen.fillAmount = 1;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Out();
        }
    }
}
