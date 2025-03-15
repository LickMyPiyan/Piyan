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
        StartCoroutine(LoadOutAndSwitchScene("Map"));
    }

    public IEnumerator LoadIn()
    {
        Loading.SetActive(true);
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
        yield return StartCoroutine(LoadingQuit());
        yield return new WaitUntil(() => LoadingScreen.fillAmount == 1);
        SceneManager.LoadScene(sceneName);
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

    public IEnumerator SpawnMob(string MobName, int Num, float MobSpawnRange)
    {
        Loading.transform.SetParent(null);
        for (int i = 0; i < Num; i++)
        {
            GameObject Mob = Instantiate(Resources.Load(MobName), new Vector3(Random.Range(-MobSpawnRange, MobSpawnRange), Random.Range(-MobSpawnRange, MobSpawnRange), 0), Quaternion.identity) as GameObject;
            GameObject healthBar = Instantiate(Resources.Load("MobHealth"), Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(GameObject.Find("Canvas").transform);

            HealthBar MobHealth = healthBar.GetComponent<HealthBar>();
            MobHealth.target = Mob.transform;
            MobHealth.offset = new Vector3(0, -1, 0);
        }
        Loading.transform.SetParent(GameObject.Find("Canvas").transform);
        yield return null;
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
