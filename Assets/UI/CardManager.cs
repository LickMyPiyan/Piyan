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
    public static List<string> UsableCards = new List<string>{"HpPlus","AtkPlus","SpdPlus","ASpdPlus","Revive"};
    public static List<string> AvailableCards;
    public static List<string> CardsOwned;
    private List<Vector3> CardsOwnedPos;
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

    void AvailableCardsTweak()
    {
        AvailableCards = new List<string>{};
        AvailableCards.AddRange(SwordCards);
        AvailableCards.AddRange(BowCards);
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AvailableCardsTweak();
        SetCardPos();
        ShowCardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
