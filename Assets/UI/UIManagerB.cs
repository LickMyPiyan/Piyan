using UnityEngine;

public class UIManagerB : MonoBehaviour
{
    public GameObject PauseUI;

    public void Paused()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseUI.activeSelf)
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !PauseUI.activeSelf)
        {
            Paused();
        }
    }
}
