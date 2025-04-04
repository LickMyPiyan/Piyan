using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Cardseffect : MonoBehaviour
{   
    public void Regeneration(int n)
    {
        if (Player.PlayerHealth < Player.PlayerMaxHealth - n*20.0f)
        {
            Player.PlayerHealth += n*20.0f;
        }
        else
        {
            Player.PlayerHealth = Player.PlayerMaxHealth;
        }
    }

    public void AtkBoost(int n)
    {
        Player.PlayerAtkBoost = Player.PlayerDefaultStats[1] + n*0.2f;
    }

    public void SpdBoost(int n)
    {
        Player.PlayerCardSpeed = Player.PlayerDefaultStats[2] + n*0.2f;
    }

    public void ASpdBoost(int n)
    {
        Player.PlayerASpd = Player.PlayerDefaultStats[3] * Mathf.Pow(0.5f, n);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CardManager.StackableCards.Count; i++)
        {
            if (CardManager.CardsOwned.Contains(CardManager.StackableCards[i]))
            {
                Debug.Log("card index: " + i);
                switch (i)
                {
                    case 0 :
                        Regeneration(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("Regeneration")]);
                        break;
                    case 1 :
                        AtkBoost(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("AtkBoost")]);
                        break;
                    case 2 :
                        SpdBoost(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("SpdBoost")]);
                        break;
                    case 3 :
                        ASpdBoost(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("ASpdBoost")]);
                        break;
                    default:
                        Debug.Log("card index: " + i);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
