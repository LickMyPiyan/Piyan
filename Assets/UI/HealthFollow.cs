using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHealth;
    Camera MainCamera;

    //讓血條跟著玩家
    void FollowPlayer()
    {
        if (Player != null)
        {
            Vector3 screenPosition = MainCamera.WorldToScreenPoint(Player.transform.position + new Vector3(0, -1, 0));
            PlayerHealth.GetComponent<RectTransform>().position = screenPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
        Player = GameObject.Find("Player");
        PlayerHealth = GameObject.Find("PlayerHealth");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
