using UnityEngine;

public class PlayerDMG : MonoBehaviour
{
    public int PlayerHealth = 100;
    public void TakePlayerDMG(int damage)
    {
        PlayerHealth -= damage;
    }
    void Die()
    {
        if(PlayerHealth <= 0)
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
        Debug.Log("Player Health: " + PlayerHealth);
    }
}
