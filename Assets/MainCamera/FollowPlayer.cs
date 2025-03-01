using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    //跟隨玩家
    void Follow()
    {
        //取得玩家物件
        Transform Player = GameObject.Find("Player").transform;
        //跟隨
        transform.position += new Vector3(Player.position.x - transform.position.x, Player.position.y - transform.position.y, 0) * Time.deltaTime * Vector2.Distance(Player.position, transform.position);
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
