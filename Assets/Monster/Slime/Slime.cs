using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float SlimeATKRange = 1.5f;
    public float SlimeATKDMG = 10.0f;
    public float SlimeMovimgSpeed = 1.0f;
    public float SlimeHealth = 100;
    public float SlimeBeforeATKTime = 0.5f;
    public float SlimeAfterATKTime = 0.5f;
    GameObject player;
    float SlimeATKTimerB = 0;
    float SlimeATKTimerA = 0;
    bool SlimeAttack = false;
    bool Aing = false;


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

    void MoveAndAttack(GameObject player)
    {
        if (Vector3.Distance(player.transform.position, transform.position) > SlimeATKRange && Aing == false)
        {
            // 追著玩家跑
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);   
            transform.position += new Vector3((player.transform.position.x - transform.position.x) / Vector3.Distance(transform.position, player.transform.position), (player.transform.position.y - transform.position.y) / Vector3.Distance(transform.position, player.transform.position), 0) * SlimeMovimgSpeed * Time.deltaTime;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= SlimeATKRange && !SlimeAttack)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            SlimeATKTimerA = Time.time;
            SlimeAttack = true;
        }

        if (SlimeAttack && Time.time - SlimeATKTimerA >= SlimeBeforeATKTime && !Aing)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
            player.GetComponent<Player>().TakePlayerDMG(SlimeATKDMG);
            SlimeATKTimerB = Time.time;
            Aing = true;
        }

        if (Aing && Time.time - SlimeATKTimerB >= SlimeAfterATKTime)
        {
            SlimeAttack = false;
            Aing = false;
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
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
        MoveAndAttack(player);
        Die();
    }
}
