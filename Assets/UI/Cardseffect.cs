using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Cardseffect: MonoBehaviour
{   
    public static void Regeneration()
    {
        if (Player.PlayerHealth < Player.PlayerMaxHealth - 10 * CardManager.CardsCount[CardManager.CardsOwned.IndexOf("Regeneration")])
        {
            Player.PlayerHealth += 10 * CardManager.CardsCount[CardManager.CardsOwned.IndexOf("Regeneration")];
        }
        else
        {
            Player.PlayerHealth = Player.PlayerMaxHealth;
        }
    }

    public static void AtkBoost()
    {
        Coefficient.AtkC += 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("AtkBoost")]);
    }

    public static void SpdBoost()
    {
        Coefficient.SpdC += 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("SpdBoost")]);
    }

    public static void ASpdBoost()
    {
        Coefficient.ASpdC += 0.2f*(CardManager.CardsCount[CardManager.CardsOwned.IndexOf("ASpdBoost")]);
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

    public static void AtkPlus()
    {
        Coefficient.AtkC += 0.5f * CardManager.TempEffect.Count(x => x.Item1 == "AtkPlus");
    }

    public static void SpdPlus()
    {
        Coefficient.SpdC += 0.5f * CardManager.TempEffect.Count(x => x.Item1 == "SpdPlus");
    }

    public static void ASpdPlus()
    {
        Coefficient.ASpdC += 0.5f * CardManager.TempEffect.Count(x => x.Item1 == "ASpdPlus");
    }
}
