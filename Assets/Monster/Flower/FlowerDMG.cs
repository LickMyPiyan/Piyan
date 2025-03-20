using UnityEngine;

public class FlowerDMG : MonoBehaviour
{
    public int FlowerHealth = 100;
    public void TakeFlowerDMG(int damage)
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }
}
