using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManagerB : MonoBehaviour
{
    public Image Bar2;
    public GameObject Loading;
    public LoadScenes loadScenes;
    public CardManager cardManager;
    public Player player;
    private float CurrentHealth;
    private float MaxHealth;
    public GameObject PauseUI;
    public GameObject WinUI;
    public GameObject DropUI;
    public List<string> CardsDropped;

    private void connect()
    {
        Bar2 = FindAndAssign<Image>("Bar2");
        Loading = FindAndAssign("Loading");
        loadScenes = FindAndAssign<LoadScenes>("UIManagerB");
        cardManager = FindAndAssign<CardManager>("UIManagerB");
        player = FindAndAssign<Player>("Player");
        PauseUI = FindAndAssign("PauseUI");
        WinUI = FindAndAssign("WinUI");
        DropUI = FindAndAssign("DropUI");

        if (PauseUI != null)
        {
            var resumeButton = PauseUI.transform.Find("Resume")?.GetComponent<Button>();
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(this.Resume);
            }

            var quitButton = PauseUI.transform.Find("Quit")?.GetComponent<Button>();
            if (quitButton != null)
            {
                quitButton.onClick.AddListener(this.Quit);
            }
        }

        if (WinUI != null)
        {
            var nextButton = WinUI.transform.Find("Next")?.GetComponent<Button>();
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(this.Next);
            }
        }
    }

    private T FindAndAssign<T>(string name) where T : Component
    {
        var obj = GameObject.Find(name);
        if (obj != null)
        {
            return obj.GetComponent<T>();
        }
        Debug.LogError($"GameObject '{name}' not found or missing '{typeof(T).Name}' component.");
        return null;
    }

    private GameObject FindAndAssign(string name)
    {
        var obj = GameObject.Find(name);
        if (obj == null)
        {
            Debug.LogError($"GameObject '{name}' not found.");
        }
        return obj;
    }

    public void HealthFill()
    {
        Bar2.fillAmount = Player.PlayerHealth / Player.PlayerMaxHealth ;
    }

    public void Paused()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        player.enabled = false;
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        player.enabled = true;
    }

    public void Quit()
    {
        StartCoroutine(loadScenes.LoadOutAndSwitchScene("Result"));
    }

    void win()
    {
        PauseUI.SetActive(false);
        WinUI.SetActive(true);
        Loading.SetActive(true);
        Loading.GetComponent<Image>().fillAmount = 0;
        Loading.transform.SetParent(GameObject.Find("MobHealthBars").transform);
        player.enabled = false;
    }

    void DetermineDrop()
    {
        CardsDropped.Clear();
        List<int> picker = new List<int>{};

        if (CardManager.AvailableCards.Count >= 3)
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
            while (picker.Count < 3)
            {
                int value = Random.Range(0, CardManager.AvailableCards.Count);
                picker.Add(value);
            }
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
            GameObject claim = Instantiate(Resources.Load<GameObject>($"Cards/Claim"), Vector3.zero, Quaternion.identity);

            Card.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 1);
            Card.GetComponent<RectTransform>().position =  new Vector3(500*i - 500 + Screen.width/2, Screen.height/2, 0);
            claim.GetComponent<RectTransform>().position = Card.GetComponent<RectTransform>().position + new Vector3(0, -345, 0);

            claim.GetComponent<Claim>().targetcard = CardsDropped[i];

            claim.transform.SetParent(DropUI.transform);
            Card.transform.SetParent(DropUI.transform);
        }
        DropUI.SetActive(true);
        WinUI.SetActive(false);
    }

    void ShowUI()
    {
        if (Win.ifwin == false)
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
