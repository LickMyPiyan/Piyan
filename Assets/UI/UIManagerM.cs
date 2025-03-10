using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerM : MonoBehaviour
{
    //抓場景裡的物件
    public GameObject StartUI;
    public GameObject Battle01;
    public GameObject EnterB01;
    public GameObject Event01;
    public GameObject EnterE01;
    public GameObject Shop01;
    public GameObject EnterS01;
    //Loading畫面
    public GameObject Loading;
    public Image LoadingScreen;
    public float transitionDuration = 0.3f;
    //抓滑鼠位置
    private Vector3 MousePosI;
    private Vector3 MousePosF;
    //設定相機移動範圍
    public Vector3 minCameraPosition = new Vector3( 0 , 0, -10);
    public Vector3 maxCameraPosition = new Vector3( 10 , 6, -10);
    //遊戲進度計數(static讓它在切場景時不變)
    public static int GameState;
    //UI跟物件配對清單
    private List<(GameObject, GameObject)> followPairs;

    //宣告攝影機
    Camera MainCamera;


    //更新UI狀態
    void UpdateUIState()
    {
        EnterB01.GetComponent<Button>().interactable = (GameState == 0);
        EnterE01.GetComponent<Button>().interactable = (GameState == 1);
        EnterS01.GetComponent<Button>().interactable = (GameState == 2);
    }
    
    //開關進節點UI
    public void ButtonPressed()
    {
        StartUI.SetActive(true);
    }

    public void UnPressed()
    {
        StartUI.SetActive(false);
    }

    //按開始可以進不同的節點
    public void StartPressed()
    {
        switch (GameState)
        {
            case 0:
                StartCoroutine(LoadOutAndSwitchScene("Battle01"));
            break;

            case 1:
                StartCoroutine(LoadOutAndSwitchScene("Event01"));
            break;

            case 2:
                StartCoroutine(LoadOutAndSwitchScene("Shop01"));
            break;
        }
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

    //節點UI移動
    void Follow()
    {
        foreach (var (gameObject, uiElement) in followPairs)
        {
            Vector3 position = MainCamera.WorldToScreenPoint(gameObject.transform.position);
            uiElement.GetComponent<RectTransform>().position = position;
        }
    }

    //滑鼠拉相機
    void CameraMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosI = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MousePosF = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            MainCamera.transform.position += MousePosI - MousePosF;

            MainCamera.transform.position = new Vector3
            (
                Mathf.Clamp(MainCamera.transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(MainCamera.transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(MainCamera.transform.position.z, minCameraPosition.z, maxCameraPosition.z)
            );
        }

    }

    void Start()
    {
        //宣告UI跟物件配對
        followPairs = new List<(GameObject, GameObject)>
        {
            (Battle01, EnterB01),
            (Event01, EnterE01),
            (Shop01, EnterS01)
        };

        StartCoroutine(LoadIn());

        MainCamera = Camera.main;

        UpdateUIState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnPressed();
        }

        Follow();
        
        CameraMove();
    }
}
