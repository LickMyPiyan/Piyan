using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CardManager: MonoBehaviour
{
    public GameObject CardUI;

    //卡片池
    public static List<string> StackableCards = new List<string>{"Regeneration","AtkBoost","SpdBoost","ASpdBoost"};
    public static List<string> UsableCards = new List<string>{"HpPlus","AtkPlus","SpdPlus","ASpdPlus"};
    public static List<string> AutoCards = new List<string>{"Revive"};

    //統計持有的卡片、金幣及相關資訊
    public static List<string> CardsOwned;
    public static List<string> AvailableCards;
    public static List<int> CardsCount;
    private List<Vector3> CardsOwnedPos;
    public static int Coin;

    //一次性卡片的效果及持續時間(回合數)
    public static List<(string, int)> TempEffect = new List<(string, int)>();

    void ShowCardUI()
    {   
        for (int i = 0; i < CardsOwned.Count; i++)
        {
            //找持有的卡片的Prefab 如果沒有就跳過
            GameObject cardPrefab = Resources.Load<GameObject>($"Cards/{CardsOwned[i]}");
            if (cardPrefab == null)
            {
                Debug.LogError($"Card prefab not found: Cards/{CardsOwned[i]}");
                continue;
            }

            //生成卡片物件
            GameObject Card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            Card.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
            Card.GetComponent<RectTransform>().position = CardsOwnedPos[i] + new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Card.transform.SetParent(CardUI.transform);

            //卡片有兩張以上的話就顯示數量
            if (CardsCount[i] > 1)
            {
                TextMeshProUGUI CountText = Instantiate(Resources.Load<TextMeshProUGUI>($"Cards/Count"), Vector3.zero, Quaternion.identity);
                CountText.text = $"{CardsCount[i]}";
                CountText.transform.SetParent(Card.transform);
                CountText.GetComponent<RectTransform>().anchoredPosition = new Vector3(50,-42,0);
            }
        }
    }

    //設定卡片位置
    void SetCardPos()
    {
        CardsOwnedPos = new List<Vector3>();
        //少於6張卡片的話就橫排&&置中
        if (CardsOwned.Count <= 6)
        {
            for (int i = 0; i < 6; i++)
            {
                CardsOwnedPos.Add(new Vector3(i * 220 - (CardsOwned.Count - 1) * 110, 140, 0));
            }
        }
        //多於6張卡片的話就換行&&不置中
        else
        {
            for (int i = 0; i < 6; i++)
            {
                CardsOwnedPos.Add(new Vector3(i * 220 - 550, 140, 0));
            }
            for (int i = 6; i < CardsOwned.Count; i++)
            {
                CardsOwnedPos.Add(CardsOwnedPos[i - 6] + new Vector3(0, -260, 0));
            }
        }
    }

    //調整局內剩餘卡片池
    public void AvailableCardsTweak()
    {
        AvailableCards = new List<string>{};

        //加入不可堆疊的卡片(暫時沒有)

        //移除已經擁有的卡片
        for (int i = 0; i <CardsOwned.Count; i++)
        {
            if (AvailableCards.Contains(CardsOwned[i]) == true)
            {
                AvailableCards.Remove(CardsOwned[i]);
            }
        }

        //加入可堆疊的卡片
        AvailableCards.AddRange(StackableCards);
        AvailableCards.AddRange(UsableCards);
    }

    //使用(真)一次性卡片
    public void UseCard(string cardname)
    {
        //使用卡片的效果
        switch (cardname)
        {
            case "HpPlus":
                Cardseffect.HpPlus();
                break;
            default:
                Debug.LogError($"Card effect not implemented for: {cardname}");
                break;
        }
        
        //消耗卡片
        if (CardsCount[CardsOwned.IndexOf(cardname)] == 1)
        {
            CardsCount.RemoveAt(CardsOwned.IndexOf(cardname));
            CardsOwned.Remove(cardname);
        }
        else
        {
            CardsCount[CardsOwned.IndexOf(cardname)] -= 1;
        }

        //刷新卡片UI
        RefreshCardUI();
    }

    //使用給時效的一次性卡片
    public void UseCardWithDuration(string cardname)
    {   
        //卡片對應的持續回合數
        var cardDuration = new Dictionary<string, int>
            {
                {"AtkPlus", 3},
                {"SpdPlus", 3},
                {"ASpdPlus", 3},
            }
        ;
        
        //加入時效
        TempEffect.Add((cardname, cardDuration[cardname]));

        //消耗卡片&&刷新UI
        if (CardsCount[CardsOwned.IndexOf(cardname)] == 1)
        {
            CardsCount.RemoveAt(CardsOwned.IndexOf(cardname));
            CardsOwned.Remove(cardname);
        }
        else
        {
            CardsCount[CardsOwned.IndexOf(cardname)] -= 1;
        }
        RefreshCardUI();
    }

    //檢查並移除時效
    //只在GameState改變時檢查一次
    public int LastGameState = 0;
    public void CheckAndRemoveTempEffects()
    {
        if (UIManagerM.GameState != LastGameState)
        {
            //重置LastGameState
            LastGameState = UIManagerM.GameState;

            if (TempEffect != null && TempEffect.Count > 0)
            {
                //更新剩餘持續回合數及移除時效
                //用foreach會炸掉 因為List在迴圈進行中被修改
                //所以要用for由後往前刪除
                for (int i = TempEffect.Count - 1; i >= 0; i--)
                {
                    var (Card, last) = TempEffect[i];
                    if (last == 1)
                    {
                        TempEffect.RemoveAt(i);
                    }
                    else
                    {
                        TempEffect[i] = (Card, last - 1);
                    }
                }
            }
        }
    }

    //刷新卡片UI
    public void RefreshCardUI()
    {
        //刪除舊的卡片UI&&重新生成
        foreach (Transform child in CardUI.transform)
        {
            if (child.name != "CardUIBoard" && child.name != "Button")
            {
                Destroy(child.gameObject);
            }
        }
        AvailableCardsTweak();
        SetCardPos();
        ShowCardUI();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CardUI == null)
        {
            CardUI = GameObject.Find("CardUI");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
