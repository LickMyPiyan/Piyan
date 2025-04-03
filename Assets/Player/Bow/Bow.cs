using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    static public float BowDMG = 40.0f;
    static public float BowMaxHoldTime = 3.0f;
    static public float BowATKDMG;
    float MouseTimer = 0.0f;

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseTimer = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - MouseTimer < BowMaxHoldTime)
            {
                BowATKDMG = (Time.time - MouseTimer) * BowDMG; 
            }
            else if (Time.time - MouseTimer >= BowMaxHoldTime)
            {
                BowATKDMG = BowMaxHoldTime * BowDMG;
            }
            Instantiate(Resources.Load("Arrow"), transform.position, Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
