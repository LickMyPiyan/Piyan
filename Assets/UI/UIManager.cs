using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public LoadScenes LoadScenes;
    void Start()
    {
        StartCoroutine(LoadScenes.LoadIn());
    }
}
