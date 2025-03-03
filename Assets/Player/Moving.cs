using UnityEngine;

public class Moving : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    public float DashDistance = 0.3f;
    public float DashCooldown = 3.0f;
    public float DashTimer = 0.0f;

    void Move()
    {
        //移動
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * PlayerSpeed * Time.deltaTime;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dash();
    }
}
