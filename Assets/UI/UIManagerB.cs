using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIManagerB : MonoBehaviour
{
    public Image Bar2;
    public LoadScenes loadScenes;
    public CardManager cardManager;
    public PlayerDMG playerDMG;
    public PlayerMoving playerMoving;
    private float CurrentHealth;
    private float MaxHealth;
    public GameObject PauseUI;
    public GameObject WinUI;
    public GameObject DropUI;
    public List<string> CardsDropped;

    private void connect()
    {
        Bar2 = GameObject.Find("Bar2").GetComponent<Image>();
        loadScenes = GameObject.Find("UIManagerB").GetComponent<LoadScenes>();
        cardManager = GameObject.Find("UIManagerB").GetComponent<CardManager>();
        playerDMG = GameObject.Find("Player").GetComponent<PlayerDMG>();
        playerMoving = GameObject.Find("Player").GetComponent<PlayerMoving>();
        PauseUI = GameObject.Find("PauseUI");
        WinUI = GameObject.Find("WinUI");
        DropUI = GameObject.Find("DropUI");

        PauseUI.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(this.Resume);

        PauseUI.transform.Find("Quit").GetComponent<Button>().onClick.AddListener(this.Quit);

        WinUI.transform.Find("Next").GetComponent<Button>().onClick.AddListener(this.Next);
    }

    public void HealthFill()
    {
        CurrentHealth = playerDMG.PlayerHealth;
        MaxHealth = playerDMG.PlayerMaxHealth;
        Bar2.fillAmount = CurrentHealth / MaxHealth;
    }

    public void Paused()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        playerMoving.enabled = false;
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        playerMoving.enabled = true;
    }

    public void Quit()
    {
        StartCoroutine(loadScenes.LoadOutAndSwitchScene("Main Menu"));
        UIManagerM.GameState = 0;
    }

    void win()
    {
        PauseUI.SetActive(false);
        WinUI.SetActive(true);
        Time.timeScale = 0;
        playerMoving.enabled = false;
    }

    void DetermineDrop()
    {
        CardsDropped.Clear();
        List<int> picker = new List<int>{};

        if (CardManager.AvailableCards.Count >3)
        {
            while (picker.Count < 3)
            {
                int value = Random.Range(0, CardManager.AvailableCards.Count);
                if (!picker.Contains(value))
                {
                    picker.Add(value);
                }
            }
        }
        else 
        {
            Debug.Log("AvailableCards is null");
        }

        for (int i = 0; i < picker.Count; i++)
        {
            CardsDropped.Add(CardManager.AvailableCards[picker[i]]);
        }
    }

    public void Next()
    {
        for (int i = 0; i < CardsDropped.Count; i++)
        {
            GameObject Card = Instantiate(Resources.Load<GameObject>($"Cards/{CardsDropped[i]}"), Vector3.zero, Quaternion.identity);
            GameObject Claim = Instantiate(Resources.Load<GameObject>($"Cards/Claim"), Vector3.zero, Quaternion.identity);

            Card.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 1);
            Card.GetComponent<RectTransform>().position =  new Vector3(500*i - 500 + Screen.width/2, Screen.height/2, 0);
            Claim.GetComponent<RectTransform>().position = Card.GetComponent<RectTransform>().position + new Vector3(0, -345, 0);

            Claim.transform.SetParent(DropUI.transform);
            Card.transform.SetParent(DropUI.transform);
        }
        DropUI.SetActive(true);
        WinUI.SetActive(false);
    }

    void ShowUI()
    {
        if (playerDMG.win == false)
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
        else if (!DropUI.activeSelf)
        {
            win();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        connect();
        DetermineDrop();
        PauseUI.SetActive(false);
        WinUI.SetActive(false);
        DropUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthFill();
        ShowUI();
    }
}
