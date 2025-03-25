using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Claim : MonoBehaviour
{
    public GameObject Loading;
    public Image LoadingScreen;
    public Player playerDMG;
    public float transitionDuration = 0.3f;
    public string targetcard;

    private IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        Loading.SetActive(true);
        LoadingScreen.fillAmount = 0;
        Loading.transform.SetParent(GameObject.Find("Canvas").transform);
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

    public void ClaimCard()
    {
        Debug.Log($"{CardManager.CardsOwned[0]},{CardManager.CardsOwned[1]},{CardManager.CardsOwned[2]}");
        CardManager.CardsOwned.Add(targetcard);
        Debug.Log($"{CardManager.CardsOwned[3]}");
        UIManagerM.GameState++;
        StartCoroutine(this.LoadOutAndSwitchScene("Map"));
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDMG = GameObject.Find("Player").GetComponent<Player>();
        Loading = GameObject.Find("Loading");
        LoadingScreen = GameObject.Find("Loading").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
