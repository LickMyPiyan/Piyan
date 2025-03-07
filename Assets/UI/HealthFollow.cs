using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHealth;
    public GameObject Slime;
    public GameObject SlimeHealth;
    Camera MainCamera;

    void FollowPlayer()
    {
        Vector3 screenPosition = MainCamera.WorldToScreenPoint(Player.transform.position + new Vector3(0, -1, 0));
        PlayerHealth.GetComponent<RectTransform>().position = screenPosition;
    }

    void FollowMob()
    {
        Vector3 screenPosition = MainCamera.WorldToScreenPoint(Slime.transform.position + new Vector3(0, 1, 0));
        SlimeHealth.GetComponent<RectTransform>().position = screenPosition;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
