using UnityEngine;

public class SlimeMoving : MonoBehaviour
{
    public ALLDATA data;
    float Timer = 0;
    Transform player = GameObject.Find("Player").transform;
    void Move()
    {
        if(Time.time - Timer > data.SlimeJumpCD)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10.0f);
            Timer = Time.time;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
