using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public LoadScenes LoadScenes;
    void Start()
    {
        StartCoroutine(LoadScenes.LoadIn());
        
        Player.PlayerMaxHealth = Player.DefaultStats[0];
        Player.PlayerHealth = Player.PlayerMaxHealth;
        Player.PlayerAtkBoost = Player.DefaultStats[1];
        Player.PlayerSpeed = Player.DefaultStats[2];
        Player.PlayerASpd = Player.DefaultStats[3];
        Player.DashDistance = Player.DefaultStats[4];
        Player.DashCooldown = Player.DefaultStats[5];

        UIManagerM.GameState = 0;
        CardManager.Coin = 0;
        CardManager.CardsOwned = new List<string>();
        CardManager.CardsCount = new List<int>();
        CardManager.CardsOwned.Clear();
        CardManager.CardsCount.Clear();
    }
}
