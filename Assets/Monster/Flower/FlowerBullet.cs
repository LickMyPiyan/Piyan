using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    private Transform Target;
    public int FlowerDMG = 10;
    public float FlowerBulletSpeed = 5.0f;
    public float BulletDestroyDistance = 10.0f;
    void TrackPlayer(Transform target)
    {
        transform.position += new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0).normalized * FlowerBulletSpeed * Time.deltaTime;
    }
    void Destroy()
    {
        if(Vector3.Distance(Target.position, transform.position) > BulletDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDMG>().TakePlayerDMG(FlowerDMG);
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
        Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer(Target);
        Destroy();
    }
}
