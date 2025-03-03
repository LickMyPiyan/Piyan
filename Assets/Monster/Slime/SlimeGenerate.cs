using UnityEngine;

public class SlimeGenerate : MonoBehaviour
{
    public int num = 3;
    public float SpawnRange = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < num; i++)
        {
            Instantiate(Resources.Load("Slime"), new Vector3(Random.Range(-SpawnRange, SpawnRange), Random.Range(-SpawnRange,SpawnRange), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
