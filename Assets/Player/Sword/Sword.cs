using UnityEngine;
public class Sword : MonoBehaviour
{
    public float SwordAttackCD = 0.5f;
    public int SwordDamage = 100;
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

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Enemy" && Input.GetMouseButton(0) && Time.time - Timer >= SwordAttackCD)
        {
            other.gameObject.GetComponent<SlimeDMG>().TakeSlimeDMG(SwordDamage);
            Timer = Time.time;
        }
    }
}
