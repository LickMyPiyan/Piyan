using UnityEngine;

public class Flower : MonoBehaviour
{
    public float FlowerATKRange = 3.0f;
    public float FlowerATKCD = 1.0f;
    public float FlowerSpeed = 1.0f;
    public float FlowerHealth = 100.0f;
    float FlowerATKTimer = 0;
    public void TakeFlowerDMG(float damage)
    {
        FlowerHealth -= damage;
    }
    void Die()
    {
        if(FlowerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FlowerAttack(GameObject flowerbullets, Transform player)
    {
        if(Win.ifwin == false &&Time.time - FlowerATKTimer >= FlowerATKCD && Vector3.Distance(player.position, transform.position) <= FlowerATKRange)
        {
            Instantiate(flowerbullets, transform.position, Quaternion.identity);
            FlowerATKTimer = Time.time;
        }
        else if(Win.ifwin == false && Vector3.Distance(player.position, transform.position) > FlowerATKRange)
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
        Die();
    }
}
