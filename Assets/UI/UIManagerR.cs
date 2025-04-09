using System;
using UnityEngine;

public class UIManagerR : MonoBehaviour
{
    private TimeSpan ExpBonusTime = new TimeSpan(0, 0, 10, 0);
    private float ExpC = 1.0f;
    public static float Exp = 0;

    void Calculate()
    {
        MainManager.GameEndTime = DateTime.Now;
        TimeSpan timeSpan = MainManager.GameEndTime - MainManager.GameStartTime;
        if (timeSpan < ExpBonusTime)
        {
            Exp += 40 * ExpC;
        }
        else
        {
            Exp += 20 * ExpC;
        }

        Exp += CardManager.Coin * 2f * ExpC;
        Exp += CardManager.CardsOwned.Count * 4f * ExpC;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (MainManager.Ending)
        {
            case 0:
                ExpC = 0.5f;
                break;
            case 1:
                ExpC = 1.0f;
                break;
            default:
                Debug.Log("Invalid ending value: " + MainManager.Ending);
                break;
        }

        Calculate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
