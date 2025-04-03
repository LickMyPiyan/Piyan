using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
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

    public void MapPressed()
    {
        int startcard = Random.Range(0,CardManager.StackableCards.Count);
        int startcard2 = Random.Range(0,CardManager.SwordCards.Count);
        int startcard3 = Random.Range(0,CardManager.UsableCards.Count);

        CardManager.CardsOwned = new List<string>{CardManager.StackableCards[startcard],CardManager.SwordCards[startcard2],CardManager.UsableCards[startcard3]};
        CardManager.CardsCount = new List<int>{1, 1, 1};

        CardManager.Coin = 0;
        
        StartCoroutine(LoadOutAndSwitchScene("Map"));
    }

    public void BacktoMenu()
    {
        UIManagerM.GameState = 0;
        ResetPlayerStats();
        CardManager.Coin = 0;
        CardManager.CardsOwned.Clear();
        CardManager.CardsCount.Clear();
        StartCoroutine(LoadOutAndSwitchScene("MainMenu"));
    }

    public void ResetPlayerStats()
    {
        
    }

    public IEnumerator LoadIn()
    {
        Loading.SetActive(true);
        LoadingScreen.fillAmount = 0;
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            LoadingScreen.fillAmount = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Loading.SetActive(false); 
    }

    public IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        Loading.SetActive(true);
        Loading.transform.SetParent(GameObject.Find("Canvas").transform);
        LoadingScreen.fillAmount = 0;
        LoadingScreen.fillOrigin = 1;
        Time.timeScale = 1;

        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            LoadingScreen.fillAmount = Mathf.Lerp(0, 1, timer / transitionDuration);
            yield return null;
        }

        LoadingScreen.fillAmount = 1;
        SceneManager.LoadScene(sceneName);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Loading = GameObject.Find("Loading");
        LoadingScreen = GameObject.Find("Loading").GetComponent<Image>();
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
