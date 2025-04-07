using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CardManager: MonoBehaviour
{
    public static List<string> StackableCards = new List<string>{"Regeneration","AtkBoost","SpdBoost","ASpdBoost"};
    public static List<string> SwordCards = new List<string>{"SlowDown","Vampirism","BroadRange","AtkCountUp","Knockback"};
    public static List<string> BowCards = new List<string>{"ChargeAtkUp","ChargeUnSlow","QuickCharge","MultiFire","Punch"};
    public static List<string> UsableCards = new List<string>{"HpPlus","AtkPlus","SpdPlus","ASpdPlus"};
    public static List<string> AutoCards = new List<string>{"Revive"};
    public static List<string> AvailableCards;
    public static List<string> CardsOwned;
    public static List<int> CardsCount;
    public Cardseffect Cardseffect;
    public static List<(string, int)> TempEffect = new List<(string, int)>();
    private List<Vector3> CardsOwnedPos;
    public static int Coin;
    public GameObject CardUI;

    void ShowCardUI()
    {   
        for (int i = 0; i < CardsOwned.Count; i++)
        {
            GameObject cardPrefab = Resources.Load<GameObject>($"Cards/{CardsOwned[i]}");
            if (cardPrefab == null)
            {
                Debug.LogError($"Card prefab not found: Cards/{CardsOwned[i]}");
                continue;
            }

            GameObject Card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            Card.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
            Card.GetComponent<RectTransform>().position = CardsOwnedPos[i] + new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Card.transform.SetParent(CardUI.transform);

            if (CardsCount[i] > 1)
            {
                TextMeshProUGUI CountText = Instantiate(Resources.Load<TextMeshProUGUI>($"Cards/Count"), Vector3.zero, Quaternion.identity);
                CountText.text = $"{CardsCount[i]}";
                CountText.transform.SetParent(Card.transform);
                CountText.GetComponent<RectTransform>().anchoredPosition = new Vector3(50,-42,0);
            }
        }
    }

    void SetCardPos()
    {
        CardsOwnedPos = new List<Vector3>();

        if (CardsOwned.Count <= 6)
        {
            for (int i = 0; i < 6; i++)
            {
                CardsOwnedPos.Add(new Vector3(i * 220 - (CardsOwned.Count - 1) * 110, 140, 0));
            }
        }
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

    public void AvailableCardsTweak()
    {
        AvailableCards = new List<string>{};
        for (int i = 0; i <CardsOwned.Count; i++)
        {
            if (AvailableCards.Contains(CardsOwned[i]) == true)
            {
                AvailableCards.Remove(CardsOwned[i]);
            }
        }
        AvailableCards.AddRange(StackableCards);
        AvailableCards.AddRange(UsableCards);
    }

    public void UseCard(string cardname)
    {
        if (CardsCount[CardsOwned.IndexOf(cardname)] == 1)
        {
            switch (cardname)
            {
                case "HpPlus":
                    Cardseffect.HpPlus();
                    break;
                default:
                    Debug.LogError($"Card effect not implemented for: {cardname}");
                    break;
            }
            CardsCount.RemoveAt(CardsOwned.IndexOf(cardname));
            CardsOwned.Remove(cardname);
        }
        else
        {
            switch (cardname)
            {
                case "HpPlus":
                    Cardseffect.HpPlus();
                    break;
                default:
                    Debug.LogError($"Card effect not implemented for: {cardname}");
                    break;
            }
            CardsCount[CardsOwned.IndexOf(cardname)] -= 1;
        }
        RefreshCardUI();
    }

    public void UseCardWithDuration(string cardname)
    {   
        int duration;

        switch (cardname)
        {
            default:
                duration = 3;
                break;
        }
        
        if (CardsCount[CardsOwned.IndexOf(cardname)] == 1)
        {
            TempEffect.Add((cardname, duration));
            CardsCount.RemoveAt(CardsOwned.IndexOf(cardname));
            CardsOwned.Remove(cardname);
            RefreshCardUI();
        }
        else
        {
            TempEffect.Add((cardname, duration));
            CardsCount[CardsOwned.IndexOf(cardname)] -= 1;
            RefreshCardUI();
        }
    }

    public int LastGameState = 0;
    public void CheckAndRemoveTempEffects()
    {
        if (UIManagerM.GameState != LastGameState)
        {
            LastGameState = UIManagerM.GameState;
            foreach (var (Card, last) in TempEffect)
            {
                if (last == 1)
                {
                    TempEffect.Remove((Card, last));
                }
                else
                {
                    TempEffect[TempEffect.IndexOf((Card, last))] = (Card, last - 1);
                }
            }
        }
    }

    public void RefreshCardUI()
    {
        foreach (Transform child in CardUI.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Card UI refreshed.");
        AvailableCardsTweak();
        SetCardPos();
        ShowCardUI();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cardseffect = GameObject.Find("UIManagerM").GetComponent<Cardseffect>();
        if (CardUI == null)
        {
            CardUI = GameObject.Find("CardUI");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CardsOwned.ForEach(card => Debug.Log(card));
            CardsCount.ForEach(count => Debug.Log(count));
            TempEffect.ForEach(effect => Debug.Log(effect));
        }
    }
}
