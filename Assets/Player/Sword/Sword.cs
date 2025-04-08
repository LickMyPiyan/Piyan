using UnityEngine;
public class Sword : MonoBehaviour
{
    float SwordAtk = 15.0f;
    float SwordAtkCD = 0.5f;
    float Timer = 0;
    void RotateToMouse()
    {
        //以螢幕中心為原點的座標系統
        Vector2 MousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        //計算角度(適用於上半部)
        float Angle = Vector2.Angle(Vector2.right, MousePos)  - 135;
        //下半部
        if (MousePos.y < 0)
        {
            Angle = 360 - Angle + 90;
        }
        //旋轉
        transform.rotation = Quaternion.Euler(0, 0, Angle);
    }

    void HitMob(Collider2D gameObject)
    {
        float SwordAtkDmg = SwordAtk * Coefficient.AtkC * Coefficient.MAtkC;
        foreach (string name in Player.MonsterName)
        {
            if (gameObject != null && 
            gameObject.CompareTag(name) && 
            Input.GetMouseButton(0) && 
            Time.time - Timer >= SwordAtkCD * Player.PlayerASpd * Coefficient.ASpdC)
            {
                switch (name)
                {
                    case "Slime":
                        gameObject.GetComponent<Slime>().TakeSlimeDMG(SwordAtkDmg);
                        gameObject.GetComponent<Slime>().SlimePauseAttack = true;
                        break;
                    case "Flower":
                        gameObject.GetComponent<Flower>().TakeFlowerDMG(SwordAtkDmg);
                        break;
                    case "Goblin":
                        gameObject.GetComponent<Goblin>().TakeGoblinDMG(SwordAtkDmg);
                        break;
                }
                Timer = Time.time;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        HitMob(other);
    }
}
