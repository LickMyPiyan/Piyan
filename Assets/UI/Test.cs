using UnityEngine;

public class Test : MonoBehaviour
{
    public void Rotate()
    {
        transform.Rotate(0, 0, Time.deltaTime * 36);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
}
