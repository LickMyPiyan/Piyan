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
    public Vector3 minCameraPosition = new Vector3( 0 , 0, -10);
    public Vector3 maxCameraPosition = new Vector3( 10 , 6, -10);
    Camera MainCamera;

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

            MainCamera.transform.position = new Vector3
            (
                Mathf.Clamp(MainCamera.transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(MainCamera.transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(MainCamera.transform.position.z, minCameraPosition.z, maxCameraPosition.z)
            );
        }

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }
}
