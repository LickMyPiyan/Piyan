using UnityEngine;

public class PlayerDMG : MonoBehaviour
{
    public float PlayerHealth = 100.0f;
    bool win = false;
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
    void Win()
    {
        if(GameObject.FindGameObjectsWithTag("Flower").Length == 0 && GameObject.FindGameObjectsWithTag("Slime").Length == 0 && win == false)
        {
            Debug.Log("You Win!");
            win = true;
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
        Win();
    }
}
