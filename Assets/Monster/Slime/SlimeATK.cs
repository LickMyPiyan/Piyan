using UnityEngine;

public class SlimeATK : MonoBehaviour
{
    public float SlimeATKRange = 1.5f;
    public float SlimeATKCD = 1.0f;
    public int SlimeATKDMG = 10;
    float Timer = 0;
    void SlimeAttack()
    {
        if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < SlimeATKRange && Time.time - Timer >= SlimeATKCD)
        {
            GameObject.Find("Player").GetComponent<PlayerDMG>().TakePlayerDMG(SlimeATKDMG);
            Timer = Time.time;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SlimeAttack();
    }
}
