using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerM : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject Battle01;
    public GameObject EnterB01;
    public Vector3 MousePosI;
    public Vector3 MousePosF;
    public Vector3 minCameraPosition = new Vector3( 0 , 0, -10);
    public Vector3 maxCameraPosition = new Vector3( 10 , 6, -10);

    Camera MainCamera;


    public void BattlePressed()
    {
        StartUI.SetActive(true);
    }

    public void BattleUnPressed()
    {
        StartUI.SetActive(false);
    }

    public void StartPressed()
    {
        SceneManager.LoadScene("BattleP01");
    }

    void Follow()
    {
        Vector3 screenPosition = MainCamera.WorldToScreenPoint(Battle01.transform.position);
        EnterB01.GetComponent<RectTransform>().position = screenPosition;
    }

    void CameraMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosI = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MousePosF = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            MainCamera.transform.position += MousePosI - MousePosF;

            MainCamera.transform.position = new Vector3
            (
                Mathf.Clamp(MainCamera.transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(MainCamera.transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(MainCamera.transform.position.z, minCameraPosition.z, maxCameraPosition.z)
            );
        }

    }

    void Start()
    {
        StartUI.SetActive(false);

        MainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BattleUnPressed();
        }
        Follow();
        CameraMove();
    }
}
