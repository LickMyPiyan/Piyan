using UnityEngine;

public class PlayerDMG : MonoBehaviour
{
    public float PlayerHealth = 100;
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
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && win == false)
        {
            Debug.Log("You Win!");
            win = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth = PlayerPrefs.GetFloat("Hp");
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        Win();
    }
}
