using UnityEngine;

public class AtkVeiw : MonoBehaviour
{
    public float MouseXvalue;
    public float MouseYvalue;
    public float Zrotation;
    void rotate()
    {
        //搞到滑鼠對應遊戲內的座標
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseXvalue = mousePosition.x;
        MouseYvalue = mousePosition.y;
        //算z軸角度tan值
        Zrotation = (MouseYvalue - transform.position.y) / (MouseXvalue - transform.position.x);
        //滑鼠在角色右側
        if (MouseXvalue - transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(Zrotation)/Mathf.PI*180 - 135);
        }
        //滑鼠在角色左側
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(Zrotation)/Mathf.PI*180 + 45);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }
}
