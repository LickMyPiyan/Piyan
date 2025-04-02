using UnityEngine;

public class Wall : MonoBehaviour
{
    Vector3 wall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = wall;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = wall;
    }
}
