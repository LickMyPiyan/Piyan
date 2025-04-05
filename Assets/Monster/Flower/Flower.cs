using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float FlowerATKRange = 3.0f;
    public float FlowerATKCD = 2.0f;
    public float FlowerMovingSpeed = 1.0f;
    public float FlowerHealth = 100.0f;
    public float FlowerMaxHealth = 100.0f;
    public float FlowerSwitchMode = 0.3f;
    public float FlowerEscapeDistance = 5.0f;
    bool Attacking = false;
    float FlowerATKTimer = 0;
    public void TakeFlowerDMG(float damage)
    {
        FlowerHealth -= damage;
    }
    void Die()
    {
        if(FlowerHealth <= 0)
        {
            Destroy(gameObject); return;
        }
        if (GameObject.Find("Player") == null)
        {
            Destroy(gameObject); return;
        }
    }
    IEnumerator Attack(GameObject flowerbullets)
    {
        Instantiate(flowerbullets, transform.position, Quaternion.identity);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
        yield return new WaitForSeconds(FlowerATKCD);
        Attacking = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(222, 0, 222, 255);
        yield return null;
    }
    void FlowerAttackAndMove(GameObject flowerbullets, Transform player)
    {
        if (FlowerHealth / FlowerMaxHealth > FlowerSwitchMode)
        {
            if (Time.time - FlowerATKTimer >= FlowerATKCD &&
                Vector3.Distance(player.position, transform.position) <= FlowerATKRange &&
                !Attacking)
            {
                FlowerATKTimer = Time.time;
                Attacking = true;
                StartCoroutine(Attack(flowerbullets));
                return;
            }
            else if(Vector3.Distance(player.position, transform.position) > FlowerATKRange &&
                    !Attacking)
            {
                transform.position += Vector3.Normalize(player.transform.position - transform.position) * FlowerMovingSpeed * Time.deltaTime;
                return;
            }
        }
        else if (FlowerHealth / FlowerMaxHealth <= FlowerSwitchMode)
        {
            if (Time.time - FlowerATKTimer >= FlowerATKCD &&
                Vector3.Distance(player.position, transform.position) >= FlowerEscapeDistance &&
                !Attacking)
            {
                FlowerATKTimer = Time.time;
                Attacking = true;
                StartCoroutine(Attack(flowerbullets));
                return;
            }
            else if (Vector3.Distance(player.position, transform.position) < FlowerEscapeDistance &&
                    !Attacking)
            {
                transform.position += Vector3.Normalize(transform.position - player.transform.position) * FlowerMovingSpeed * Time.deltaTime;
                return;
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(222, 0, 222, 255);
    }

    // Update is called once per frame
    void Update()
    {
        FlowerAttackAndMove(Resources.Load("FlowerBullet") as GameObject, GameObject.Find("Player").transform);
        Die();
    }
}
