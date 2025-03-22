using UnityEngine;

public class PlayerDMG : MonoBehaviour
{
    public float PlayerMaxHealth = 100.0f;
    public float PlayerHealth = 100.0f;
    public bool win = false;
    public void TakePlayerDMG(int damage)
    {
        PlayerHealth -= damage;
    }
    void Die()
    {
        if(PlayerHealth <= 0)
        {
            Destroy(gameObject);
            PlayerPrefs.SetFloat("Hp", PlayerMaxHealth);
        }
    }
    void Win()
    {
        if(GameObject.FindGameObjectsWithTag("Flower").Length == 0 && GameObject.FindGameObjectsWithTag("Slime").Length == 0 && win == false && Time.time > 1)
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
