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
    private void Out()
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
        StartCoroutine(LoadOutAndSwitchScene("Map"));
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
        LoadingScreen.fillAmount = 0;
        float Timer = 0f;
        while (Timer < transitionDuration)
        {
            LoadingScreen.fillOrigin = 1;
            LoadingScreen.fillAmount = Mathf.Lerp(0, 1, Timer / transitionDuration);
            Timer += Time.deltaTime;
            yield return null;
        }
        LoadingScreen.fillAmount = 1;

        yield return new WaitUntil(() => LoadingScreen.fillAmount == 1);
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
