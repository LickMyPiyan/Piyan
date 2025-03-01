using UnityEngine;

public class Moving : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    public float DashDistance = 0.5f;
    public float DashCooldown = 3.0f;
    public float DashTimer = 0.0f;
    public float PlayerWidth = 0.5f;

    void Move()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * PlayerSpeed * Time.deltaTime;
    }
    void Dash()
    {
        Vector3 MousePos = new Vector3(Input.mousePosition.x - 960, Input.mousePosition.y - 540, 0);
        Vector3 Posf = Vector3.Normalize(MousePos) * DashDistance;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Posf);
        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null && Vector2.Distance(transform.position, hit.point) < DashDistance && Time.time - DashTimer > DashCooldown)
        {
            transform.position = new Vector3(hit.point.x, hit.point.y, 0);
            DashTimer = Time.time;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && Time.time - DashTimer > DashCooldown)
        {
            transform.position += Posf;
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
