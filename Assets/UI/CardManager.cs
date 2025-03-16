using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<string> StackableCards = new List<string>{"Regenaration","AtkBoost","SpdBoost","ASpdBoost"};
    public List<string> SwordCards = new List<string>{"SlowDown","Vampirism","BroadRange","AtkCountUp","Knockback"};
    public List<string> BowCards = new List<string>{"ChargeAtkUp","ChargeUnSlow","QuickCharge","MultiFire","Punch"};
    public List<string> UsableCards = new List<string>{"HpPlus","AtkPlus","SpdPlus","ASpdPlus","Revive"};
    public List<string> CardsOwned;
    public List<Vector3> CardsOwnedPos = new List<Vector3>{new Vector3(-450,90,0),new Vector3(-270,90,0),new Vector3(-90,90,0),new Vector3(90,90,0),new Vector3(270,90,0),new Vector3(450,90,0)};
    public GameObject CardUI;

    void ShowCardUI()
    {
        for (int i = 0; i < CardsOwned.Count; i++)
        {
            GameObject Card = Instantiate(Resources.Load($"Cards/{CardsOwned[i]}"), Vector3.zero, Quaternion.identity) as GameObject;
            Card.GetComponent<RectTransform>().position = CardsOwnedPos[i] + new Vector3(Screen.width/2, Screen.height/2, 0);
            Card.transform.SetParent(CardUI.transform);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int tester1 = Random.Range(0,StackableCards.Count);
        int tester2 = Random.Range(0,SwordCards.Count);
        int tester3 = Random.Range(0,UsableCards.Count);
        CardsOwned = new List<string>{StackableCards[tester1],SwordCards[tester2],UsableCards[tester3]};

        if (CardsOwned.Count > 6)
        {
            for (int i = 6; i < CardsOwned.Count; i++)
            {
                CardsOwnedPos.Add(CardsOwnedPos[i-6] + new Vector3(0,-180,0));
            }
        }

        ShowCardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
