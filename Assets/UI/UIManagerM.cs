using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerM : MonoBehaviour
{
    public LoadScenes LoadScenes;
    public CardManager CardManager;
    //抓場景裡的物件
    public GameObject BasicUI;
    public GameObject StartUI;
    public GameObject CardUI;
    public TextMeshProUGUI StageName;
    public TextMeshProUGUI CoinCount;
    public GameObject EnterB01, EnterE01, EnterS01, 
                    EnterB02, EnterE02, EnterS02, 
                    EnterB03, EnterE03, EnterS03,
                    EnterBB;
    public static List<GameObject> EnterButtons;
    public float transitionDuration = 0.3f;
    //遊戲進度計數(static讓它在切場景時不變)
    public static int GameState = 0;
    //UI跟座標配對清單
    private List<(Vector3, GameObject)> followPairs;
    //場景名稱清單
    private List<string> BattleScenes = new List<string> { "BattleP01", "BattleF01", "BattleF02", "BattleS01", "BattleT01", "BossBattle01" };
    private List<string> EventScenes = new List<string> { "EventP01", "EventP02", "EventF01", "EventS01", "EventT01" };
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

        CoinCount.text = $"{CardManager.Coin}";
    }
    
    //開關進節點UI
    public void ButtonPressed()
    {
        SaveCamera();
        StartUI.SetActive(true);
        StageName.text = PickedScenes[GameState];
        MainCamera.GetComponent<DragCamera>().enabled = false;
        BasicUI.SetActive(false);
    }

    public void CardPressed()
    {
        CardUI.SetActive(true);
        MainCamera.GetComponent<DragCamera>().enabled = false;
        BasicUI.SetActive(false);
    }

    public void UnPressed()
    {
        StartUI.SetActive(false);
        CardUI.SetActive(false);
        MainCamera.GetComponent<DragCamera>().enabled = true;
        BasicUI.SetActive(true);
    }

    //按開始可以進不同的節點
    public void StartPressed()
    {
        StartCoroutine(LoadScenes.LoadOutAndSwitchScene(PickedScenes[GameState]));
    }

    void SaveCamera()
    {
        PlayerPrefs.SetFloat("zoom", MainCamera.orthographicSize);
        PlayerPrefs.SetFloat("cameraX", MainCamera.transform.position.x); 
        PlayerPrefs.SetFloat("cameraY", MainCamera.transform.position.y);
    }

    void LoadCamera()
    {
        MainCamera.transform.position = new Vector3(PlayerPrefs.GetFloat("cameraX"), PlayerPrefs.GetFloat("cameraY"), -10);
        MainCamera.orthographicSize = PlayerPrefs.GetFloat("zoom");
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
                                            EnterB03, EnterE03, EnterS03,
                                            EnterBB};

        //按鈕和對應位置的配對
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
            (new Vector3(14,2,0), EnterS03),
            (new Vector3(14,8,0), EnterBB)
        };

        //隨機挑選場景清單
        int PickBF = Random.Range(1, 3);
        int PickEP = Random.Range(0, 2);
        PickedScenes = new List<string>();
        PickedScenes.AddRange(new List<string> { BattleScenes[0], EventScenes[PickEP], ShopScenes[0], 
                                                BattleScenes[PickBF], EventScenes[2], ShopScenes[1],
                                                BattleScenes[3], EventScenes[3], ShopScenes[2],
                                                //BattleScenes[4], EventScenes[4], ShopScenes[3], 
                                                BattleScenes[5] });

        StartUI.SetActive(false);
        CardUI.SetActive(false);

        MainCamera = Camera.main;
        CardManager = GameObject.Find("UIManagerM").GetComponent<CardManager>();

        UpdateUIState();

        LoadCamera();

        CardManager.CheckAndRemoveTempEffects();
        CardManager.RefreshCardUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnPressed();
        }

        if (BasicUI.activeSelf)
        {
            GameObject.Find("Bar2").GetComponent<Image>().fillAmount = Player.PlayerHealth / Player.PlayerMaxHealth;
        }
        
        Follow();
    }
}
