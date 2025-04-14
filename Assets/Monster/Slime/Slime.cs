using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    static public float SlimeATKDMG = 10.0f;
    static public float SlimeMovingSpeed = 1.0f;
    public float SlimeHealth = SlimeMaxHealth;
    static public float SlimeMaxHealth = 100.0f;
    static public float SlimeDodgeChance = 0.5f;
    static public float SlimeDodgeHealth = 0.5f;
    static public float SlimeTrackDistance = 2.0f;
    static public float SlimeAttackDistance = 3.0f;
    static public float SlimeBeforeAttackTime = 1.0f;
    static public float SlimeAfterAttackTime = 1.0f;
    static public float SlimePauseAttackTime = 1.0f;
    public bool SlimePauseAttack = false;
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
        //玩家死掉時跟著死掉，讓遊戲清空
        if(SlimeHealth <= 0 || GameObject.Find("Player") == null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BeforeAttack(GameObject player)
    {
        //前搖結束前若玩家離開攻擊範圍或史萊姆被玩家攻擊則取消本次攻擊
        while (Time.time - AttackingTimer < SlimeBeforeAttackTime)
        {
            if (SlimePauseAttack ||
                UnityEngine.Vector3.Distance(player.transform.position, transform.position) > SlimeAttackDistance)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator Attack(GameObject player)
    {
        //這裡是攻擊前搖
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        yield return StartCoroutine(BeforeAttack(player));
        if (UnityEngine.Vector3.Distance(player.transform.position, transform.position) <= SlimeAttackDistance &&
            !SlimePauseAttack)
        {
            //攻擊前搖結束若玩家仍在範圍內則造成傷害並進入攻擊後搖
            player.GetComponent<Player>().TakePlayerDMG(SlimeATKDMG);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
            yield return new WaitForSeconds(SlimeAfterAttackTime);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            Attacking = false;
            yield return null;
        }
        else if (UnityEngine.Vector3.Distance(player.transform.position, transform.position) > SlimeAttackDistance &&
                !SlimePauseAttack)
        {
            //攻擊前搖結束若玩家離開攻擊範圍則取消攻擊
            Attacking = false;
            yield return null;
        }
        else if (SlimePauseAttack)
        {
            Attacking = false;
            SlimePauseAttack = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
            yield return new WaitForSeconds(SlimePauseAttackTime);
        }
    }

    void MoveAndAttack(GameObject player)
    {
        if (UnityEngine.Vector3.Distance(player.transform.position, transform.position) > SlimeAttackDistance &&
            !Attacking)
        {
            //當玩家位於攻擊範圍外則追蹤玩家
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            transform.position += UnityEngine.Vector3.Normalize(player.transform.position - transform.position) * SlimeMovimgSpeed * Time.deltaTime;
            Attacking = false;
            SlimePauseAttack = false;
        }
        else if (UnityEngine.Vector3.Distance(player.transform.position, transform.position) <= SlimeAttackDistance &&
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
        Die();
        MoveAndAttack(GameObject.Find("Player"));
    }
}
