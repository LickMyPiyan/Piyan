using UnityEngine;
using UnityEngine.UI;

public class UIManagerB : MonoBehaviour
{
    public Image PlayerHealth;
    public float MaxHealth = 100f;
    public float CurrentHealth = 100f;
    //都先用100 能讀到再說
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
        PlayerHealth.fillAmount = CurrentHealth / MaxHealth;

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
