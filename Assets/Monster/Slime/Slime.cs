using UnityEngine;

public class Slime : MonoBehaviour
{
    public float SlimeATKRange = 1.5f;
    public float SlimeATKCD = 1.0f;
    public float SlimeATKDMG = 10.0f;
    public float SlimeMovimgSpeed = 1.0f;
    public float SlimeHealth = 100;
    float SlimeATKTimer = 0;
    public void TakeSlimeDMG(float damage)
    {
        SlimeHealth -= damage;
    }
    void Die()
    {
        if(SlimeHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Move(Transform player)
    {
        if (Win.ifwin == false && Vector3.Distance(player.position, transform.position) > SlimeATKRange)
        {
            //追著玩家跑
            transform.position += new Vector3((player.position.x - transform.position.x) / Vector3.Distance(transform.position, player.position), (player.position.y - transform.position.y) / Vector3.Distance(transform.position, player.position), 0) * SlimeMovimgSpeed * Time.deltaTime;
        }
        else if (Win.ifwin == false && Vector3.Distance(player.position, transform.position) <= SlimeATKRange && Time.time - SlimeATKTimer > SlimeATKCD)
        {
            /*在這播攻擊動畫@李彥甫*/
            player.GetComponent<Player>().TakePlayerDMG(SlimeATKDMG);
            SlimeATKTimer = Time.time;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(GameObject.Find("Player").transform);
        Die();
    }
}
