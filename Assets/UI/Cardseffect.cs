using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Cardseffect : MonoBehaviour
{   
    public void Regeneration(int n)
    {
        if (Player.PlayerHealth < Player.PlayerMaxHealth - 10*n)
        {
            Player.PlayerHealth += 10*n;
        }
        else
        {
            Player.PlayerHealth = Player.PlayerMaxHealth;
        }
    }

    public void HpPlus()
    {
        if (Player.PlayerHealth < Player.PlayerMaxHealth - 30)
        {
            Player.PlayerHealth += 30;
        }
        else
        {
            Player.PlayerHealth = Player.PlayerMaxHealth;
        }
    }

    public void AtkBoostTweak()
    {
        Player.PlayerAtkBoost = Player.PlayerDefaultStats[1] + 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("AtkBoost")])
                                                       + 0.5f*(CardManager.TempEffect.Count(x => x.Item1 == "AtkPlus"));
    }

    public void CardSpeedTweak()
    {
        Player.PlayerCardSpeed = Player.PlayerDefaultStats[2] + 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("SpdBoost")])
                                                              + 0.5f*(CardManager.TempEffect.Count(x => x.Item1 == "SpdPlus"));
    }

    public void ASpdTweak()
    {
        Player.PlayerASpd = Player.PlayerDefaultStats[3] * Mathf.Pow(0.5f,(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("ASpdBoost")]))
                                                         * Mathf.Pow(0.3f, CardManager.TempEffect.Count(x => x.Item1 == "ASpdPlus"));
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CardManager.CardsOwned.Count; i++)
        {
            switch (CardManager.CardsOwned[i])
            {
                case "Regeneration":
                    Regeneration(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("Regeneration")]);
                    break;
                default:
                    Debug.LogError($"Card effect not implemented for: {CardManager.CardsOwned[0]}");
                    break;
            }
        }
        
        AtkBoostTweak();
        CardSpeedTweak();
        ASpdTweak();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
