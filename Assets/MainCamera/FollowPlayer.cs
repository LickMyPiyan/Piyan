using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    public float FollowSpeed = 3.0f;
    //跟隨玩家
    void Follow()
    {
        if (GameObject.Find("Player") != null)
        {
            //取得玩家物件
            Transform Player = GameObject.Find("Player").transform;
            //跟隨
            transform.position += new Vector3(Player.position.x - transform.position.x, Player.position.y - transform.position.y, 0) * FollowSpeed * Time.deltaTime;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
}
