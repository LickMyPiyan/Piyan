using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Cardseffect: MonoBehaviour
{   
    public static void Regeneration(int n)
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

    public static void HpPlus()
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

    public static void AtkCTweak()
    {
        Coefficient.AtkC = 1 + 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("AtkBoost")]) + 0.5f*(CardManager.TempEffect.Count(x => x.Item1 == "AtkPlus"));
    }

    public static void SpdCTweak()
    {
        Coefficient.SpdC = 1 + 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("SpdBoost")]) + 0.5f*(CardManager.TempEffect.Count(x => x.Item1 == "SpdPlus"));
    }

    public static void ASpdCTweak()
    {
        Coefficient.ASpdC = Mathf.Pow(0.5f,(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("ASpdBoost")])) * Mathf.Pow(0.3f, CardManager.TempEffect.Count(x => x.Item1 == "ASpdPlus"));
    }
}
