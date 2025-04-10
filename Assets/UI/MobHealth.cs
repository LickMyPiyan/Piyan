using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    private RectTransform rectTransform;
    private Camera mainCamera;
    public Image healthBar;

    private float currentHealth;
    private float maxHealth;

    void FollowMob()
    {
        //跟隨怪物
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.transform.position + offset);
        rectTransform.position = screenPos;
    }

    void UpdateHealth(string targetName)
    {
        //更新血條(適配不同怪物)
        switch (targetName)
        {
            case "Slime":
                currentHealth = target.GetComponent<Slime>().SlimeHealth;
                maxHealth = Slime.SlimeMaxHealth;
                break;
            case "Flower":
                currentHealth = target.GetComponent<Flower>().FlowerHealth;
                maxHealth = Flower.FlowerMaxHealth;
                break;
            case "Goblin":
                currentHealth = target.GetComponent<Goblin>().GoblinHealth;
                maxHealth = Goblin.GoblinMaxHealth;
                break;
            default:
                Debug.LogError("Unknown target name: " + targetName);
                break;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        healthBar = transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (target != null)
        {
            FollowMob();
            UpdateHealth(target.name.Replace("(Clone)", ""));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
