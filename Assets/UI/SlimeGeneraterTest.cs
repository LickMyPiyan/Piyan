using UnityEngine;

public class SlimeGeneraterTest : MonoBehaviour
{
    public LoadScenes LoadScenes;
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public GameObject MobHealth;

    void Start()
    {
        StartCoroutine(LoadScenes.SpawnMob("Slime", SlimeNum, SlimeSpawnRange));
    }

    void Update()
    {
        
    }
}
