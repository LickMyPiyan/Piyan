using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject Loading;
    public Image LoadingScreen;
    public float transitionDuration = 0.3f;
    
    public void MapPressed()
    {
        StartCoroutine(LoadOutAndSwitchScene("Map"));
    }

    private IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(LoadOut());
        yield return new WaitUntil(() => LoadingScreen.fillAmount == 1);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            LoadingScreen.fillAmount = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LoadingScreen.fillAmount = 0;
        Loading.SetActive(false);
    }

    public IEnumerator LoadOut()
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

    void Start()
    {
        StartCoroutine(LoadIn());
    }
}
