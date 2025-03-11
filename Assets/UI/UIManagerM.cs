using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerM : MonoBehaviour
{
    //抓場景裡的物件
    public GameObject StartUI;
    public TextMeshProUGUI StageName;
    public GameObject EnterB01, EnterE01, EnterS01, 
                    EnterB02, EnterE02, EnterS02, 
                    EnterB03, EnterE03, EnterS03;
    public static List<GameObject> EnterButtons;
    //Loading畫面
    public GameObject Loading;
    public Image LoadingScreen;
    public float transitionDuration = 0.3f;
    //遊戲進度計數(static讓它在切場景時不變)
    public static int GameState;
    //UI跟座標配對清單
    private List<(Vector3, GameObject)> followPairs;
    //場景名稱清單
    private List<string> BattleScenes = new List<string> { "BattleP01", "BattleF01", "BattleF02", "BattleS01", "BattleT01", "BossBattle01" };
    private List<string> EventScenes = new List<string> { "EventP01", "EventP02", "EventF01", "Events01", "EventT01" };
    private List<string> ShopScenes = new List<string> { "ShopP01", "ShopF01", "ShopS01", "ShopT01" };
    //選擇場景清單
    private List<string> PickedScenes;

    //宣告攝影機
    Camera MainCamera;


    //更新UI狀態
    void UpdateUIState()
    {
        var buttons = EnterButtons;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null)
            {
                var button = buttons[i].GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = (GameState == i);
                }
            }
        }
    }
    
    //開關進節點UI
    public void ButtonPressed()
    {
        if (GameState >= 0 && GameState < PickedScenes.Count)
        {
            StartUI.SetActive(true);
            StageName.text = PickedScenes[GameState];
        }
    }

    public void UnPressed()
    {
        StartUI.SetActive(false);
    }

    //按開始可以進不同的節點
    public void StartPressed()
    {
        StartCoroutine(LoadOutAndSwitchScene(PickedScenes[GameState]));
    }

    //離開節點切場景
    private IEnumerator LoadOutAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(LoadOut());
        yield return new WaitUntil(() => LoadingScreen.fillAmount == 1);
        SceneManager.LoadScene(sceneName);
    }

    //進入節點動畫
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

    //離開節點動畫
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
        foreach (var (position, Button) in followPairs)
        {
            var rectTransform = Button.GetComponent<RectTransform>();
            Vector3 screenposition = MainCamera.WorldToScreenPoint(position);
            rectTransform.position = screenposition;
        }
    }

    void Start()
    {
        EnterButtons = new List<GameObject> {EnterB01, EnterE01, EnterS01,
                                            EnterB02, EnterE02, EnterS02, 
                                            EnterB03, EnterE03, EnterS03};

        //宣告UI跟物件配對
        followPairs = new List<(Vector3, GameObject)>
        {
            (new Vector3(-6,1,0), EnterB01),
            (new Vector3(-2,-3,0), EnterE01),
            (new Vector3(2,1,0), EnterS01),
            (new Vector3(-2,6,0), EnterB02),
            (new Vector3(2,9,0), EnterE02),
            (new Vector3(6,6,0), EnterS02),
            (new Vector3(8,1,0), EnterB03),
            (new Vector3(12,-3,0), EnterE03),
            (new Vector3(14,2,0), EnterS03)
        };

        //隨機挑選場景清單
        int PickBF = Random.Range(1, 3);
        int PickEP = Random.Range(0, 2);
        PickedScenes = new List<string>();
        PickedScenes.AddRange(new List<string> { BattleScenes[0], EventScenes[PickEP], ShopScenes[0], 
                                                BattleScenes[PickBF], EventScenes[2], ShopScenes[1],
                                                BattleScenes[3], EventScenes[3], ShopScenes[2],
                                                BattleScenes[4], EventScenes[4], ShopScenes[3], BattleScenes[5] });

        StartUI.SetActive(false);

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
    }
}
