using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Globalization;

public class Player : MonoBehaviour
{
    public static float PlayerMaxHealth = 100.0f;
    public static float PlayerHealth = 100.0f;
    public static float PlayerSpd = 1.5f;
    public static float PlayerASpd = 1.0f;
    public static float DashD = 2.0f;
    public static float DashCD = 2.0f;

    public GameObject[] Weapon;
    static public string[] MonsterName = new string[] {"Slime", "Flower", "Goblin"};

    float Speed;
    float DashTimer = 0.0f;

    void Move()
    {
        if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            //走斜線時距離速度除以根號二
            Speed = PlayerSpd * Coefficient.SpdC / Mathf.Sqrt(2);
        }
        else
        {
            Speed = PlayerSpd * Coefficient.SpdC;
        }
        //移動
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Speed * Time.deltaTime;
    }
    void Dash()
    {
        //以螢幕中心為原點的座標系統
        Vector3 MousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
        //終點座標
        Vector3 Posf = Vector3.Normalize(MousePos) * DashD * Coefficient.DashDC;
        //射線檢測
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Posf);
        //撞牆的情況     
        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null && Vector2.Distance(transform.position, hit.point) < DashD * Coefficient.DashDC && Time.time - DashTimer > DashCD * Coefficient.DashCDC)
        {
            //傳送到撞牆的位置
            transform.position = new Vector3(hit.point.x, hit.point.y, 0);
            //紀錄現在時間
            DashTimer = Time.time;
        }
        //沒撞牆的情況
        else if(Input.GetKeyDown(KeyCode.Space) && Time.time - DashTimer > DashCD * Coefficient.DashCDC)
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
        //血量歸零時死亡
        if(PlayerHealth <= 0)
        {
            Destroy(gameObject);
            PlayerHealth = PlayerMaxHealth;
        }
    }
    void SetWeapon(GameObject sword, GameObject bow)
    {
        //將使用的武器放入Weapon陣列
        Weapon = new GameObject[] {sword, bow};
        //將第0個武器以外的武器都收起
        foreach(GameObject weapon in Weapon)
        {
            weapon.SetActive(false);
        }
        Weapon[0].SetActive(true);
    }
    void SwitchWeapon()
    {
        //武器切換
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach(GameObject weapon in Weapon)
            {
                //找出現在正在使用的武器
                if (weapon.activeSelf == true)
                {
                    int num = System.Array.IndexOf(Weapon, weapon);
                    if(num != (Weapon.Length - 1))
                    {
                        //當武器不是最後一個時將本武器收起拿出下一個武器
                        Weapon[num].SetActive(false); 
                        Weapon[num + 1].SetActive(true);
                        break;
                    }
                    else
                    {
                        //當武器是最後一個時收起本武器拿出第0個武器
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
        SetWeapon(GameObject.Find("SwordRange"), GameObject.Find("Bow"));
    }

    void Update()
    {
        Die();
        Move();
        Dash();
        SwitchWeapon();
    }
}