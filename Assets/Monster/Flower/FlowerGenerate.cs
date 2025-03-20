using Unity.Collections;
using UnityEngine;

public class FlowerGenerate : MonoBehaviour
{
    public int FlowerNum = 3; 
    public float FlowerSpawnRangeInner = 5.0f;
    public float FlowerSpawnRangeOuter = 10.0f;
    float xpos;
    float ypos;
    void Generate()
    {
        if(Random.value < 0.5)
        {
            xpos = Random.Range(FlowerSpawnRangeInner, FlowerSpawnRangeOuter);
        }
        else
        {
            xpos = Random.Range(-FlowerSpawnRangeOuter, -FlowerSpawnRangeInner);
        }
        if(Random.value < 0.5)
        {
            ypos = Random.Range(FlowerSpawnRangeInner, FlowerSpawnRangeOuter);
        }
        else
        {
            ypos = Random.Range(-FlowerSpawnRangeOuter, -FlowerSpawnRangeInner);
        }
        for (int i = 0; i < FlowerNum; i++)
        {
            Instantiate(Resources.Load("Flower"), new Vector3(xpos, ypos, 0), Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
