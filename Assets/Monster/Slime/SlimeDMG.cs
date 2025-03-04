using UnityEngine;

public class SlimeDMG : MonoBehaviour
{
    public ALLDATA data;
    public void TakeSlimeDMG(int damage)
    {
        data.SlimeHealth -= damage;
    }
    void Die()
    {
        if(data.SlimeHealth <= 0)
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
