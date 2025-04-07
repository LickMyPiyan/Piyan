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
        
        Player.PlayerMaxHealth = Player.PlayerDefaultStats[0];
        Player.PlayerHealth = Player.PlayerMaxHealth;
        Player.PlayerAtkBoost = Player.PlayerDefaultStats[1];
        Player.PlayerCardSpeed = Player.PlayerDefaultStats[2];
        Player.PlayerASpd = Player.PlayerDefaultStats[3];
        Player.DashDistance = Player.PlayerDefaultStats[4];
        Player.DashCooldown = Player.PlayerDefaultStats[5];

        UIManagerM.GameState = 0;
        CardManager.Coin = 0;
        CardManager.CardsOwned = new List<string>();
        CardManager.CardsCount = new List<int>();
        CardManager.TempEffect = new List<(string, int)>();
    }
}
