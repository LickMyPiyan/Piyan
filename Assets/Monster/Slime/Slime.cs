using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float SlimeATKDMG = 10.0f;
    public float SlimeMovimgSpeed = 1.0f;
    public float SlimeHealth = 100;
    public float SlimeMaxHealth = 100.0f;
    public float SlimeAttackCooldown = 1.0f;
    public float SlimeDodgeChance = 0.5f;
    GameObject player;
    float SlimeATKTimer = 0.0f;
    float v;


    public void TakeSlimeDMG(float damage)
    {
        if(SlimeHealth / SlimeMaxHealth > 0.5f)
        {
            SlimeHealth -= damage;
        }
        else if (SlimeHealth / SlimeMaxHealth <= 0.5f)
        {
            v = Random.value;
            if (v < SlimeDodgeChance)
            {
                //閃避特效
            }
            else
            {
                //受擊特效
                SlimeHealth -= damage;
            }
        }
    }
    
    void Die()
    {
        if(SlimeHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Move(GameObject player)
    {
        // 追著玩家跑  
        transform.position += new Vector3((player.transform.position.x - transform.position.x) / Vector3.Distance(transform.position, player.transform.position), (player.transform.position.y - transform.position.y) / Vector3.Distance(transform.position, player.transform.position), 0) * SlimeMovimgSpeed * Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time - SlimeATKTimer >= SlimeAttackCooldown)
        {
            // 讓Slime攻擊玩家
            collision.gameObject.GetComponent<Player>().TakePlayerDMG(SlimeATKDMG);
            SlimeATKTimer = Time.time;  
        }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
    }
    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }
        Move(player);
        Die();
    }
}
