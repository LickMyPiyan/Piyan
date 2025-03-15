using UnityEngine;

public class SlimeGenerateTest : MonoBehaviour
{
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public GameObject MobHealth;
    public Canvas uiCanvas;

    void Start()
    {
        for (int i = 0; i < SlimeNum; i++)
        {
            GameObject slime = Instantiate(Resources.Load("Slime"), new Vector3(Random.Range(-SlimeSpawnRange, SlimeSpawnRange), Random.Range(-SlimeSpawnRange, SlimeSpawnRange), 0), Quaternion.identity) as GameObject;
            GameObject healthBar = Instantiate(Resources.Load("MobHealth"), Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(uiCanvas.transform);

            HealthBar MobHealth = healthBar.GetComponent<HealthBar>();
            MobHealth.target = slime.transform;
            MobHealth.offset = new Vector3(0, -1, 0);
        }
    }

    void Update()
    {
        
    }
}
