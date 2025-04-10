using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class MainManager : MonoBehaviour
{
    public LoadScenes LoadScenes;
    public static DateTime GameStartTime;
    public static DateTime GameEndTime;
    public static int Ending = 0;

    public static int PlayerLv = 0;
    public static float PlayerExp = 0.0f;
    private float LvUpExp = 200.0f;

    public Image ExpBar;
    public TextMeshProUGUI PlayerLvText;

    //升等
    void levelup()
    {
        while (PlayerExp >= LvUpExp)
        {
            PlayerLv++;
            PlayerExp -= LvUpExp;
        }
    }
    
    //顯示等級和經驗條
    void ShowPlayerLv()
    {
        PlayerLvText.text = $"Lv.{PlayerLv}";
        ExpBar.fillAmount = PlayerExp / LvUpExp;
    }

    void Start()
    {
        StartCoroutine(LoadScenes.LoadIn());
        levelup();
        ShowPlayerLv();
    }
}
