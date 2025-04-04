using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float SlimeATKDMG = 10.0f;
    public float SlimeMovimgSpeed = 1.0f;
    public float SlimeHealth = 100;
    public float SlimeMaxHealth = 100.0f;
    public float SlimeAttackCooldown = 1.0f;
    public float SlimeDodgeChance = 0.5f;
    public float SlimeDodgeHealth = 0.5f;
    public float SlimeAttackDistance = 2.0f;
    public float SlimeBeforeAttackTime = 1.0f;
    public float SlimeAfterAttackTime = 1.0f;
    bool Attacking = false;
    float AttackingTimer = 0.0f;
    float Value;


    public void TakeSlimeDMG(float damage)
    {
        //血量百分比大於觸發閃避血量時不閃避
        if(SlimeHealth / SlimeMaxHealth > SlimeDodgeHealth)
        {
            SlimeHealth -= damage;
        }
        //血量百分比小於觸發閃避血量時根據閃避機率決定是否閃避此次攻擊
        else if (SlimeHealth / SlimeMaxHealth <= SlimeDodgeHealth)
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
        //血量歸零時死亡
        if(SlimeHealth <= 0)
        {
            Destroy(gameObject);
        }
        //玩家死掉時跟著死掉，讓遊戲清空
        if (GameObject.Find("Player") == null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Attack(GameObject player)
    {
        //這裡是攻擊前搖
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(SlimeBeforeAttackTime);
        if (Vector3.Distance(player.transform.position, transform.position) <= SlimeAttackDistance)
        {
            //攻擊前搖結束若玩家仍在範圍內則造成傷害並進入攻擊後搖
            player.GetComponent<Player>().TakePlayerDMG(SlimeATKDMG);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
            yield return new WaitForSeconds(SlimeAfterAttackTime);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            Attacking = false;
            yield return null;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > SlimeAttackDistance)
        {
            //攻擊前搖結束若玩家離開攻擊範圍則取消攻擊
            Attacking = false;
            yield return null;
        }
    }

    void MoveAndAttack(GameObject player)
    {
        if (Vector3.Distance(player.transform.position, transform.position) > SlimeAttackDistance &&
            !Attacking)
        {
            //當玩家位於攻擊範圍外則追蹤玩家
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            transform.position += Vector3.Normalize(player.transform.position - transform.position) * SlimeMovimgSpeed * Time.deltaTime;
            Attacking = false;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= SlimeAttackDistance &&
                !Attacking &&
                Time.time - AttackingTimer >= SlimeBeforeAttackTime)
        {
            //當玩家位於攻擊範圍內則進入攻擊程序
            Attacking = true;
            AttackingTimer = Time.time;
            StartCoroutine(Attack(player));
            return;
        }
    }

    void Start()
    {
        //開始時將顏色設為綠色
        GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
    }
    void Update()
    {
        MoveAndAttack(GameObject.Find("Player"));
        Die();
    }
}
