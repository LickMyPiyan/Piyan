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
    float SlimeATKTimer = 0.0f;
    float Value;


    public void TakeSlimeDMG(float damage)
    {
        if(SlimeHealth / SlimeMaxHealth > 0.5f)
        {
            SlimeHealth -= damage;
        }
        else if (SlimeHealth / SlimeMaxHealth <= 0.5f)
        {
            Value = Random.value;
            if (Value < SlimeDodgeChance)
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
        if (GameObject.Find("Player") == null)
        {
            Destroy(gameObject);
        }
    }

    void Move(GameObject player)
    {
        // 追著玩家跑     
        transform.position += Vector3.Normalize(player.transform.position - transform.position) * SlimeMovimgSpeed * Time.deltaTime;
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
        Move(GameObject.Find("Player"));
        Die();
    }
}
