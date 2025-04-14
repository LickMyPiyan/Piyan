using UnityEngine;

public class Goblin : MonoBehaviour
{
    static public float GoblinMovimgSpeed = 1.0f;
    static public float GoblinATKDMG = 10.0f;
    public float GoblinHealth = 100.0f;
    static public float GoblinMaxHealth = 100.0f;
    static public float BlockTime = 1.5f;
    static public float BlockCD = 15.0f;
    float BlockTimer = 0.0f;
    float CDTimer = 0.0f;
    bool Blocking = false;


    public void TakeGoblinDMG(float damage)
    {
        if (Blocking)
        {
            BlockTimer = Time.time; // Reset block timer
        }
        else if (Time.time - CDTimer >= BlockCD) // Check if cooldown is over
        {
            BlockTimer = Time.time; // Start block timer
            Blocking = true; // Activate blocking
        }
        else
        {
            GoblinHealth -= damage; // Take damage if not blocking
        }
    }

    void State()
    {
        if (Blocking && Time.time - BlockTimer >= BlockTime)
        {
            Blocking = false; // End blocking state
            CDTimer = Time.time; // Start cooldown timer
        }
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
        State();
        Move(GameObject.Find("Player"));
    }
}
