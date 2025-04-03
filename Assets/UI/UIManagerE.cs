using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManagerE : MonoBehaviour
{
    public GameObject CardDisplay;
    public TextMeshProUGUI CoinCount;
    public LoadScenes LoadScenes;

    public void Sure()
    {
        CardManager.Coin -= 3;

        string targetcard = CardManager.AvailableCards[Random.Range(0, CardManager.AvailableCards.Count)];
        if (CardManager.CardsOwned.Contains(targetcard))
        {
            CardManager.CardsCount[CardManager.CardsOwned.IndexOf(targetcard)]++;
        }
        else
        {
            CardManager.CardsOwned.Add(targetcard);
            CardManager.CardsCount.Add(1);
        }
        
        GameObject card = Instantiate(Resources.Load<GameObject>($"Cards/{targetcard}"), Vector3.zero, Quaternion.identity);
        card.GetComponent<RectTransform>().localScale = new Vector3(5, 5, 1);
        card.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        card.transform.SetParent(CardDisplay.transform);
        CardDisplay.SetActive(true);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadScenes = GameObject.Find("UIManagerE").GetComponent<LoadScenes>();
        CardDisplay = GameObject.Find("CardDisplay");
        CoinCount = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        CardDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CoinCount.text = $"{CardManager.Coin}";
        
        if (CardManager.Coin < 3)
        {
            GameObject.Find("Sure").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("Sure").GetComponent<Button>().interactable = true;
        }
        
        if (Input.GetMouseButtonDown(0) && CardDisplay.activeSelf)
        {
            LoadScenes.Out();
        }
    }
}
