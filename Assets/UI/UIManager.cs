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
        
        Player.PlayerHealth = Player.PlayerMaxHealth;

        UIManagerM.GameState = 0;
        CardManager.Coin = 0;
        CardManager.CardsOwned = new List<string>();
        CardManager.CardsCount = new List<int>();
        CardManager.TempEffect = new List<(string, int)>();

        Coefficient.Reset();
    }
}
