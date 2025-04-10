using System;
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
        //清空上一局的數值
        MainManager.Ending = 0;
        MainManager.Destination = 1;
        Player.PlayerHealth = Player.PlayerMaxHealth;
        UIManagerM.GameState = 0;
        CardManager.Coin = 5;
        CardManager.CardsOwned = new List<string>();
        CardManager.CardsCount = new List<int>();
        CardManager.TempEffect = new List<(string, int)>();
        Coefficient.MaxHealthC = 1.0f;
        Coefficient.Reset();
        
        //給開局卡片
        int startcard1 = UnityEngine.Random.Range(0, CardManager.StackableCards.Count);
        int startcard2 = UnityEngine.Random.Range(0, CardManager.UsableCards.Count);
        CardManager.CardsOwned.Add(CardManager.StackableCards[startcard1]);
        CardManager.CardsCount.Add(1);
        CardManager.CardsOwned.Add(CardManager.UsableCards[startcard2]);
        CardManager.CardsCount.Add(1);

        //紀錄遊戲開始時間
        MainManager.GameStartTime = DateTime.Now;

        StartCoroutine(LoadOutAndSwitchScene("Map"));
    }

    //從Result回到主選單
    public void BacktoMenu()
    {
        StartCoroutine(LoadOutAndSwitchScene("MainMenu"));
    }

    //開場動畫
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

    //離場動畫
    public IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        //確保Loading在最上層
        Loading.SetActive(true);
        Loading.transform.SetParent(GameObject.Find("Canvas").transform);
        Loading.transform.SetAsLastSibling();
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
        LoadingScreen = Loading.GetComponent<Image>();
        StartCoroutine(LoadIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
