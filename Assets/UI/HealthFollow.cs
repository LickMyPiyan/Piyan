using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHealth;
    Camera MainCamera;

    void Follow()
    {
        Vector3 screenPosition = MainCamera.WorldToScreenPoint(Player.transform.position + new Vector3(0, -1, 0));
        PlayerHealth.GetComponent<RectTransform>().position = screenPosition;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
}
