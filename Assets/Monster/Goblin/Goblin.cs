using UnityEngine;

public class Goblin : MonoBehaviour
{
    static public float GoblinMovimgSpeed = 1.0f;
    static public float GoblinATKDMG = 10.0f;
    public float GoblinHealth = 100.0f;
    static public float GoblinMaxHealth = 100.0f;


    public void TakeGoblinDMG(float damage)
    {
        GoblinHealth -= damage;
    }
    void Move(GameObject player)
    {
        // 追著玩家跑     
        transform.position += Vector3.Normalize(player.transform.position - transform.position) * GoblinMovimgSpeed * Time.deltaTime;
    }
    void Die()
    {
        if(GoblinHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (GameObject.Find("Player") == null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        Move(GameObject.Find("Player"));
    }
}
