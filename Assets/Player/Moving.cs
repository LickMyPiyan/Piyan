using UnityEngine;

public class Moving : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    public float DashDistance = 3.0f;
    public float DashCooldown = 3.0f;
    public float DashTimer = 0.0f;
        void Move()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * PlayerSpeed * Time.deltaTime;
    }
    void Dash()
    {
        Vector3 MousePos = new Vector3(Input.mousePosition.x - 960, Input.mousePosition.y - 540, 0);
        if(Input.GetKeyDown(KeyCode.Space) && Time.time - DashTimer > DashCooldown)
        {
            transform.position += new Vector3(MousePos.x, MousePos.y, 0).normalized * DashDistance;
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
