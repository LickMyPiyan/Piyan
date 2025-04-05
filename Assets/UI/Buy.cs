using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buy : MonoBehaviour
{
    int Prise = 4;
    public string targetcard;
    private bool CanBuy = true;

    public void BuyCard()
    {
        if (CardManager.CardsOwned.Contains(targetcard))
        {
            CardManager.CardsCount[CardManager.CardsOwned.IndexOf(targetcard)]++;
        }
        else
        {
            CardManager.CardsOwned.Add(targetcard);
            CardManager.CardsCount.Add(1);
        }
        
        CardManager.Coin -= Prise;
        CanBuy = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CardManager.Coin < Prise)
        {
            CanBuy = false;
        }

        GetComponent<Button>().interactable = CanBuy;
    }
}
