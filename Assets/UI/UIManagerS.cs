using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerS : MonoBehaviour
{
    public LoadScenes LoadScenes;
    public CardManager CardManager;
    
    public List<string> CardsOnSale = new List<string>();
    
    void DetermineCardsOnSale()
    {
        CardsOnSale.Clear();
        List<int> picker = new List<int>{};

        while (picker.Count < 6)
        {
            CardManager.AvailableCardsTweak();

            int value = Random.Range(0, CardManager.AvailableCards.Count);
            picker.Add(value);
        }

        for (int i = 0; i < picker.Count; i++)
        {
            CardsOnSale.Add(CardManager.AvailableCards[picker[i]]);
        }
    }

    void ShowCardsOnSale()
    {
        for (int i = 0; i < CardsOnSale.Count; i++)
        {
            GameObject Card = Instantiate(Resources.Load<GameObject>($"Cards/{CardsOnSale[i]}"), Vector3.zero, Quaternion.identity);
            GameObject buy = Instantiate(Resources.Load<GameObject>($"Cards/Buy"), Vector3.zero, Quaternion.identity);

            Card.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
            Card.GetComponent<RectTransform>().position =  new Vector3( (i % 4)*220 + Screen.width/2,250 + Screen.height/2 - (i/4)*350, 0);
            buy.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.0f);
            buy.GetComponent<RectTransform>().position = Card.GetComponent<RectTransform>().position + new Vector3(0, -180, 0);

            buy.GetComponent<Buy>().targetcard = CardsOnSale[i];

            buy.transform.SetParent(GameObject.Find("CardsOnSale").transform);
            Card.transform.SetParent(GameObject.Find("CardsOnSale").transform);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadScenes = GameObject.Find("UIManagerS").GetComponent<LoadScenes>();
        CardManager = GameObject.Find("UIManagerS").GetComponent<CardManager>();

        DetermineCardsOnSale();
        ShowCardsOnSale();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>().text = $"{CardManager.Coin}";
    }
}
