using Unity.Collections;
using UnityEngine;

public class FlowerGenerate : MonoBehaviour
{
    public int FlowerNum = 3; 
    public float FlowerSpawnRange = 10.0f;
    void Generate(GameObject flower)
    {
        for(int i = 0; i < FlowerNum; i++)
        {
            Instantiate(flower, new Vector3(Random.Range(-FlowerSpawnRange, FlowerSpawnRange), Random.Range(-FlowerSpawnRange,FlowerSpawnRange), 0), Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Generate(Resources.Load("Flower") as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
