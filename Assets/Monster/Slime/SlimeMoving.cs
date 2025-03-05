using Unity.VisualScripting;
using UnityEngine;

public class SlimeMoving : MonoBehaviour
{
    public float SlimeMovimgSpeed = 1.0f;
    void Move()
    {
        Transform player = GameObject.Find("Player").transform;
        if (player != null)
        {
            transform.position += new Vector3((player.position.x - transform.position.x) / Vector3.Distance(transform.position, player.position), (player.position.y - transform.position.y) / Vector3.Distance(transform.position, player.position), 0) * SlimeMovimgSpeed * Time.deltaTime;
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
    }
}
