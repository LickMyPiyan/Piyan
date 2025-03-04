using UnityEngine;

public class SlimeGenerate : MonoBehaviour
{
    public ALLDATA data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < data.SlimeNum; i++)
        {
            Instantiate(Resources.Load("Slime"), new Vector3(Random.Range(-data.SlimeSpawnRange, data.SlimeSpawnRange), Random.Range(-data.SlimeSpawnRange,data.SlimeSpawnRange), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
