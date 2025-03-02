using UnityEngine;

public class AtkVeiw : MonoBehaviour
{
    public float MouseXvalue;
    public float MouseYvalue;
    public float Zrotation;
    void rotate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseXvalue = mousePosition.x;
        MouseYvalue = mousePosition.y;
        if (MouseXvalue - transform.position.x > 0)
        {
            Zrotation = (MouseYvalue - transform.position.y) / (MouseXvalue - transform.position.x);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(Zrotation)/Mathf.PI*180 - 135);
        }
        else
        {
            Zrotation = (MouseYvalue - transform.position.y) / (MouseXvalue - transform.position.x);
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
