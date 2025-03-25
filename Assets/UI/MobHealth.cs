using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
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
        maxHealth = 100;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position + offset);
            rectTransform.position = screenPos;
            CurrentHealth = target.GetComponent<Slime>().SlimeHealth;
            healthBar.fillAmount = (float)CurrentHealth / maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
