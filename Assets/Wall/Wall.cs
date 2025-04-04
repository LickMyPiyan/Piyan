using UnityEngine;

public class Wall : MonoBehaviour
{
    Vector3 wallpos;
    Quaternion wallrot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallpos = transform.position;
        wallrot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = wallpos;
        transform.rotation = wallrot;
    }
}
