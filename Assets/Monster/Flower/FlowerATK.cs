using UnityEngine;

public class FlowerATK : MonoBehaviour
{
    public float FlowerATKRange = 3.0f;
    public float FlowerATKCD = 1.0f;
    public float FlowerSpeed = 1f;
    float Timer = 0;
    void FlowerAttack(GameObject flowerbullets, Transform player)
    {
        if(Time.time - Timer >= FlowerATKCD && Vector3.Distance(player.position, transform.position) <= FlowerATKRange)
        {
            Instantiate(flowerbullets, transform.position, Quaternion.identity);
            Timer = Time.time;
        }
        else if(Vector3.Distance(player.position, transform.position) > FlowerATKRange)
        {
            transform.position += new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, 0).normalized * FlowerSpeed * Time.deltaTime;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlowerAttack(Resources.Load("FlowerBullet") as GameObject, GameObject.Find("Player").transform);
    }
}
