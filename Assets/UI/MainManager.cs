using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MainManager : MonoBehaviour
{
    public LoadScenes LoadScenes;
    public static DateTime GameStartTime;
    public static DateTime GameEndTime;
    public static int Ending = 0;
    public static int Destination = 1;

    public static int PlayerLv = 0;
    private int LvExp = 0;
    public static float PlayerExp = 0.0f;

    void GainExp()
    {
        PlayerExp += UIManagerR.Exp;
        UIManagerR.Exp = 0;
        PlayerLv = Mathf.FloorToInt(PlayerExp / 200);
        LvExp = Mathf.FloorToInt(PlayerExp % 200);
    }

    void Start()
    {
        StartCoroutine(LoadScenes.LoadIn());
    }
}
