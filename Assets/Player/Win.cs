using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Win : MonoBehaviour
{
    public static bool ifwin;

    void win()
    {
        if (GameObject.FindGameObjectsWithTag("Flower").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Slime").Length == 0 &&
            ifwin == false && Time.time > 1.0f)
        {
            Debug.Log("You Win!");
            ifwin = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ifwin = false;
    }

    // Update is called once per frame
    void Update()
    {
        win();
    }
}
