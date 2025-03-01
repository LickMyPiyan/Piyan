using UnityEngine;

public class Moving : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    public float DashDistance = 5.0f;
    void Move()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * PlayerSpeed * Time.deltaTime;
    }
    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += (Input.mousePosition - transform.position) / Vector2.Distance(Input.mousePosition, transform.position) * DashDistance;
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
