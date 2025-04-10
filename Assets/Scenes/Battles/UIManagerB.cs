using System;
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
    bool check = true;

    private void connect()
    {
        Bar2 = FindAndAssign<Image>("Bar2");
        Loading = FindAndAssign("Loading");
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
        Loading.SetActive(true);
        Loading.GetComponent<Image>().fillAmount = 0;
        Loading.transform.SetParent(GameObject.Find("MobHealthBars").transform);
        player.enabled = false;
        WinUI.SetActive(true);
        
        Coefficient.Reset();
        DetermineDrop();
    }

    void DetermineDrop()
    {
        if (CardManager.AvailableCards != null)
        {
            CardsDropped.Clear();
            List<int> picker = new List<int>{};

            if (CardManager.AvailableCards.Count >= 3)
            {
                while (picker.Count < 3)
                {
                    int value = UnityEngine.Random.Range(0, CardManager.AvailableCards.Count);
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
                    int value = UnityEngine.Random.Range(0, CardManager.AvailableCards.Count);
                    picker.Add(value);
                }
            }

            for (int i = 0; i < picker.Count; i++)
            {
                CardsDropped.Add(CardManager.AvailableCards[picker[i]]);
            }
        }
        else
        {
            Debug.Log("CardManager.AvailableCards == null");
        }
    }

    public void Next()
    {
        if (UIManagerM.GameState == 9)
        {
            MainManager.Ending = MainManager.Destination;
            StartCoroutine(loadScenes.LoadOutAndSwitchScene("Result"));
            return;
        }
        
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
        else if (!DropUI.activeSelf && check)
        {
            check = false;
            win();
        }
    }

    private void ApplyCardEffects(Dictionary<string, Action> effectMap)
    {
        for (int i = 0; i < CardManager.CardsOwned.Count; i++)
        {
            if (effectMap.TryGetValue(CardManager.CardsOwned[i], out var effect))
            {
                effect();
            }
        }
    }

    private void ApplyTempEffects(Dictionary<string, Action> effectMap)
    {
        for (int i = 0; i < CardManager.TempEffect.Count; i++)
        {
            if (effectMap.TryGetValue(CardManager.TempEffect[i].Item1 , out var effect))
            {
                effect();
            }
        }
    }

    void CoefficientTweak()
    {
        if (CardManager.CardsOwned != null)
        {
            var CardsEffectDict = new Dictionary<string, Action>
            {
                {"Regeneration", Cardseffect.Regeneration},
                {"AtkBoost", Cardseffect.AtkBoost},
                {"SpdBoost", Cardseffect.SpdBoost},
                {"ASpdBoost", Cardseffect.ASpdBoost}
            };

            ApplyCardEffects(CardsEffectDict);
        }
        if (CardManager.TempEffect != null)
        {
            var TempEffectDict = new Dictionary<string, Action>
            {
                {"AtkPlus", Cardseffect.AtkPlus},
                {"SpdPlus", Cardseffect.SpdPlus},
                {"ASpdPlus", Cardseffect.ASpdPlus}
            };

            ApplyTempEffects(TempEffectDict);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        connect();
        PauseUI.SetActive(false);
        WinUI.SetActive(false);
        DropUI.SetActive(false);

        CoefficientTweak();
    }

    // Update is called once per frame
    void Update()
    {
        HealthFill();
        ShowUI();

        if (Input.GetKeyDown(KeyCode.O))
        {
            loadScenes.Out();
        }
    }
}
