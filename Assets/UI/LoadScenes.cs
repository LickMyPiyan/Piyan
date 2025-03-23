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

    public IEnumerator SpawnMob(string MobName, int Num, float MobSpawnRange)
    {
        for (int i = 0; i < Num; i++)
        {
            GameObject Mob = Instantiate(Resources.Load(MobName), new Vector3(Random.Range(-MobSpawnRange, MobSpawnRange), Random.Range(-MobSpawnRange, MobSpawnRange), 0), Quaternion.identity) as GameObject;
            GameObject healthBar = Instantiate(Resources.Load("MobHealth"), Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(GameObject.Find("MobHealthBars").transform);

            HealthBar MobHealth = healthBar.GetComponent<HealthBar>();
            MobHealth.target = Mob.transform;
            MobHealth.offset = new Vector3(0, -1, 0);
        }
        yield return null;
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
