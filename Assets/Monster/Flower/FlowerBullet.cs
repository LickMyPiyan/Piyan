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
        transform.position += new Vector3(target.x - X, target.y - Y, 0).normalized * FlowerBulletSpeed * Time.deltaTime;
    }
    void Destroy()
    {
        if(Vector3.Distance(new Vector3(TargetposX, TargetposY, 0), transform.position) > BulletDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().TakePlayerDMG(FlowerDMG);
            Destroy(gameObject);
        }
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        TargetposX = player.transform.position.x;
        TargetposY = player.transform.position.y;
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
