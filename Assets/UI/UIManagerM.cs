using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerM : MonoBehaviour
{
    public GameObject StartUI;

    public void BattlePressed()
    {
        StartUI.SetActive(true);
    }

    public void BattleUnPressed()
    {
        StartUI.SetActive(false);
    }

    public void StartPressed()
    {
        SceneManager.LoadScene("BattleP01");
    }

    void Start()
    {
        StartUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BattleUnPressed();
        }
    }
}
