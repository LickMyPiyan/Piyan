using UnityEngine;

public class TakeDMG : MonoBehaviour
{
    public int SlimeHealth = 100;
    public void TakeDamage(int damage)
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
