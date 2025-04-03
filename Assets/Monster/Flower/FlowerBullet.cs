using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    float TargetposX;
    float TargetposY;
    float X;
    float Y;
    public int FlowerDMG = 10;
    public float FlowerBulletSpeed = 5.0f;
    public float BulletDestroyDistance = 10.0f;
    GameObject player;
    void TrackPlayer(Vector3 target)
    {
        //讓子彈朝著生成時玩家位置移動
        transform.position += new Vector3(target.x - X, target.y - Y, 0).normalized * FlowerBulletSpeed * Time.deltaTime;
    }
    void Destroy()
    {
        //當子彈與玩家的距離大於銷毀距離時，銷毀子彈
        if(Vector3.Distance(new Vector3(TargetposX, TargetposY, 0), transform.position) > BulletDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //當子彈碰到玩家時，對玩家造成傷害並銷毀子彈
        if(other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().TakePlayerDMG(FlowerDMG);
            Destroy(gameObject);
            return;
        }
        //當子彈碰到牆壁時，銷毀子彈
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        //紀錄生成時玩家的位置
        TargetposX = player.transform.position.x;
        TargetposY = player.transform.position.y;
        //紀錄生成時的位置
        X = transform.position.x;
        Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer(new Vector3(TargetposX, TargetposY, 0));
        Destroy();
    }
}
