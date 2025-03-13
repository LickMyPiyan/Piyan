using UnityEngine;

public class SlimeGenerateTest : MonoBehaviour
{
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public GameObject MobHealth;
    public Transform SlimePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < SlimeNum; i++)
        {
            Instantiate(Resources.Load("Slime"), new Vector3(Random.Range(-SlimeSpawnRange, SlimeSpawnRange), Random.Range(-SlimeSpawnRange,SlimeSpawnRange), 0), Quaternion.identity);
            Instantiate(Resources.Load("MobHealth"), new Vector3(0,0,0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
