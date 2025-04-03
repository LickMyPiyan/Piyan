using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Globalization;

public class Player : MonoBehaviour
{
    public static float PlayerMaxHealth = 100.0f;
    public static float PlayerHealth = 100.0f;
    public static float PlayerAtkBoost = 1.0f;
    public static float PlayerSpeed = 1.0f;
    public static float PlayerASpd = 1.0f;
    public static float DashDistance = 2.0f;
    public static float DashCooldown = 2.0f;
    static public GameObject sword;
    static public GameObject bow;
    public GameObject[] Weapon = new GameObject[2];

    public List<float> PlayerDefaultStats = new List<float>{100.0f, 100.0f, 1.0f, 1.0f, 1.0f, 2.0f, 2.0f};
    float Speed;
    float DashTimer = 0.0f;

    void Move()
    {
        if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            //走斜線時距離速度除以根號二
            Speed = PlayerSpeed / Mathf.Sqrt(2);
        }
        else
        {
            Speed = PlayerSpeed;
        }
        //移動
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Speed * 1.5f * Time.deltaTime;
    }
    void Dash()
    {
        //以螢幕中心為原點的座標系統
        Vector3 MousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
        //終點座標
        Vector3 Posf = Vector3.Normalize(MousePos) * DashDistance;
        //射線檢測
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Posf);
        //撞牆的情況     
        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null && Vector2.Distance(transform.position, hit.point) < DashDistance && Time.time - DashTimer > DashCooldown)
        {
            //傳送到撞牆的位置
            transform.position = new Vector3(hit.point.x, hit.point.y, 0);
            //紀錄現在時間
            DashTimer = Time.time;
        }
        //沒撞牆的情況
        else if(Input.GetKeyDown(KeyCode.Space) && Time.time - DashTimer > DashCooldown)
        {
            //傳送到滑鼠位置
            transform.position += Posf;
            //紀錄現在時間
            DashTimer = Time.time;
        }
    }
    public void TakePlayerDMG(float damage)
    {
        PlayerHealth -= damage;
    }
    void Die()
    {
        if(PlayerHealth <= 0)
        {
            Destroy(gameObject);
            PlayerHealth = PlayerMaxHealth;
        }
    }
    void SwitchWeapon()
    {
        //武器切換
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach(GameObject weapon in Weapon)
            {
                if (weapon.activeSelf == true)
                {
                    int num = System.Array.IndexOf(Weapon, weapon);
                    if(num != (Weapon.Length - 1))
                    {
                        Weapon[num].SetActive(false); 
                        Weapon[num + 1].SetActive(true);
                        break;
                    }
                    else
                    {
                        Debug.Log(num);
                        Weapon[num].SetActive(false);
                        Weapon[0].SetActive(true);
                        
                    }
                }
            }
        }
    }

    void Start()
    {
        sword = GameObject.Find("SwordRange");
        bow = GameObject.Find("Bow");
        Weapon = new GameObject[] {sword, bow};
        foreach(GameObject weapon in Weapon)
        {
            weapon.SetActive(false);
        }
        Weapon[0].SetActive(true);
    }

    void Update()
    {
        Die();
        Move();
        Dash();
        SwitchWeapon();
    }
}