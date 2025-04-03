using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    private RectTransform rectTransform;
    private Camera mainCamera;
    public Image healthBar;
    private float CurrentHealth;
    private float maxHealth;

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
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.transform.position + offset);
            rectTransform.position = screenPos;

            string targetName = target.name.Replace("(Clone)", "");
            switch (targetName)
                {
                    case "Slime":
                        CurrentHealth = target.GetComponent<Slime>().SlimeHealth;
                        maxHealth = target.GetComponent<Slime>().SlimeMaxHealth;
                        break;
                    case "Flower":
                        CurrentHealth = target.GetComponent<Flower>().FlowerHealth;
                        maxHealth = target.GetComponent<Flower>().FlowerMaxHealth;
                        break;
                    case "Goblin":
                        CurrentHealth = target.GetComponent<Goblin>().GoblinHealth;
                        maxHealth = target.GetComponent<Goblin>().GoblinMaxHealth;
                        break;
                    default:
                        Debug.LogError("Unknown mob type: " + target.name);
                        break;
                }
            healthBar.fillAmount = (float)CurrentHealth / maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
