using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Cardseffect : MonoBehaviour
{
    public void Regeneration()
    {
        if (Player.PlayerHealth < Player.PlayerMaxHealth - 20.0f)
        {
            Player.PlayerHealth += 20.0f;
        }
        else
        {
            Player.PlayerHealth = Player.PlayerMaxHealth;
        }
    }

    public void AtkBoost()
    {
        Player.PlayerAtkBoost += 0.2f;
    }

    public void SpdBoost()
    {
        Player.PlayerSpeed += 0.3f;
    }

    public void ASpdBoost()
    {
        Player.PlayerASpd *= 0.5f;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CardManager.StackableCards.Count; i++)
        {
            if (CardManager.CardsOwned.Contains(CardManager.StackableCards[i]))
            {
                switch (i)
                {
                    case 0 :
                        Regeneration();
                        break;
                    case 1 :
                        AtkBoost();
                        break;
                    case 2 :
                        SpdBoost();
                        break;
                    case 3 :
                        ASpdBoost();
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
