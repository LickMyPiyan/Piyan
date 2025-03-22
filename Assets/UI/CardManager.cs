using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CardManager: MonoBehaviour
{
    private List<string> StackableCards = new List<string>{"Regeneration","AtkBoost","SpdBoost","ASpdBoost"};
    private List<string> SwordCards = new List<string>{"SlowDown","Vampirism","BroadRange","AtkCountUp","Knockback"};
    private List<string> BowCards = new List<string>{"ChargeAtkUp","ChargeUnSlow","QuickCharge","MultiFire","Punch"};
    private List<string> UsableCards = new List<string>{"HpPlus","AtkPlus","SpdPlus","ASpdPlus","Revive"};
    public static List<string> AvailableCards;
    public List<string> CardsOwned;
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
        AvailableCards.AddRange(UsableCards);
        for (int i = 0; i <CardsOwned.Count; i++)
        {
            if (AvailableCards.Contains(CardsOwned[i]) == true)
            {
                AvailableCards.Remove(CardsOwned[i]);
            }
        }
        AvailableCards.AddRange(StackableCards);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int startcard = Random.Range(0,StackableCards.Count);
        int tester2 = Random.Range(0,SwordCards.Count);
        int tester3 = Random.Range(0,UsableCards.Count);
        CardsOwned = new List<string>{StackableCards[startcard],SwordCards[tester2],UsableCards[tester3]};

        AvailableCardsTweak();
        SetCardPos();
        ShowCardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
