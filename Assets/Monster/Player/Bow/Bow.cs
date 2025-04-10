using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    float BowAtk = 8.0f;
    float BowAimSpd = 0.9f;
    float minAimAtkC = 0.3f;
    float maxAimAtkC = 3.0f;
    float AimSlowC = 0.6f;
    float BowAtkCD = 0.8f;

    float MouseTimer = 0.0f;
    float CDTimer = 0.0f;

    static public float BowAtkDmg;

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseTimer = Time.time;
            Coefficient.SpdC -= AimSlowC;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < CDTimer + BowAtkCD * Player.PlayerASpd * Coefficient.ASpdC)
            {
                Coefficient.SpdC += AimSlowC;
                return;
            }
            else
            {
                MouseTimer = Mathf.Clamp(MouseTimer, CDTimer - BowAtkCD * Player.PlayerASpd * Coefficient.ASpdC, Time.time);

                float AimAtkC = (Time.time - MouseTimer) * BowAimSpd * Coefficient.ChargeSpdC + minAimAtkC;
                AimAtkC = Mathf.Clamp(AimAtkC, minAimAtkC, maxAimAtkC);
                BowAtkDmg = AimAtkC * BowAtk * Coefficient.AtkC * Coefficient.RAtkC * Coefficient.ChargeAtkC;

                Coefficient.SpdC += AimSlowC;
                Instantiate(Resources.Load("Arrow"), transform.position, Quaternion.identity);
                CDTimer = Time.time;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
