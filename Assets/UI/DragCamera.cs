using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : MonoBehaviour
{
    //抓滑鼠位置
    private Vector3 MousePosI;
    private Vector3 MousePosF;
    //設定相機移動範圍
    public Vector3 minCameraPosition;
    public Vector3 maxCameraPosition;
    Camera MainCamera;
    private List<GameObject> EnterButtons;

    //滑鼠拉相機
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
        }

        //限制相機移動範圍
        MainCamera.transform.position = new Vector3
            (
                Mathf.Clamp(MainCamera.transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(MainCamera.transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(MainCamera.transform.position.z, minCameraPosition.z, maxCameraPosition.z)
            );

    }

    void CameraZoom()
    {
        //滑鼠滾輪縮放相機
        if (Input.mouseScrollDelta.y > 0)
        {
            MainCamera.orthographicSize -= 1;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            MainCamera.orthographicSize += 1;
        }
        //限制相機縮放範圍&&平衡相機縮放用的調整
        MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize, 4, 8);
        float MoveRange = (8-MainCamera.orthographicSize)/3;
        minCameraPosition = new Vector3(5-5*MoveRange, 3-3*MoveRange, -10);
        maxCameraPosition = new Vector3(5+5*MoveRange, 3+3*MoveRange, -10);
        ScaleButtons();
    }

    //調關卡按鍵大小(讓它雖然是UI 但看起來像地圖物件)
    void ScaleButtons()
    {
        float scaleFactor = 10 / MainCamera.orthographicSize;
        foreach (GameObject button in EnterButtons)
        {
            button.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
        EnterButtons = UIManagerM.EnterButtons;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        CameraZoom();
    }
}
