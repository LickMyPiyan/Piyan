using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerR : MonoBehaviour
{
    private TimeSpan ExpBonusTime = new TimeSpan(0, 0, 10, 0);
    private float ExpC = 1.0f;
    public static float Exp = 0;
    public TextMeshProUGUI EndingText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI CalcText;
    public TextMeshProUGUI ExpText;
    string CalcRecorder;

    //檢查達到的結局
    void CheckEnding()
    {
        switch (MainManager.Ending)
        {
            case 0:
                ExpC = 1.0f;
                EndingText.text = "Lose";
                break;
            case 1:
                ExpC = 2.0f;
                EndingText.text = "End";
                break;
            default:
                Debug.Log("Invalid ending value: " + MainManager.Ending);
                break;
        }
    }

    //結算獲得的經驗值
    void CalculateExp()
    {
        //計算遊戲完成時間
        MainManager.GameEndTime = DateTime.Now;
        TimeSpan timeSpan = MainManager.GameEndTime - MainManager.GameStartTime;
        //10分鐘內完成給更多經驗值
        if (timeSpan < ExpBonusTime)
        {
            Exp += 5 * UIManagerM.GameState * ExpC;
            CalcRecorder = $"5(time bonus) * {UIManagerM.GameState} * {ExpC}";
        }
        else
        {
            Exp += 3 * UIManagerM.GameState * ExpC;
            CalcRecorder = $"3 * {UIManagerM.GameState} * {ExpC}";
        }
        Exp += CardManager.Coin * 1f * ExpC;
        Exp += CardManager.CardsOwned.Count * 3f * ExpC;
    }

    //更新結算畫面文字
    void TextUpdate()
    {
        TimeText.text = "Game Set At:"+(MainManager.GameEndTime.ToString())+"/n"+"Used Time:"+(MainManager.GameEndTime - MainManager.GameStartTime).ToString(@"hh\:mm\:ss");
        CalcText.text = "Completion:"+CalcRecorder+"/n"+"Owned Cards:"+$"{CardManager.CardsOwned.Count}"+"/n"+"Coins:"+$"{CardManager.Coin}";
        ExpText.text = "Exp: +"+$"{Exp}";
    }

    //獲得經驗值
    void GainExp()
    {
        MainManager.PlayerExp += Exp;
        Exp = 0;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckEnding();
        CalculateExp();
        TextUpdate();
        GainExp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
