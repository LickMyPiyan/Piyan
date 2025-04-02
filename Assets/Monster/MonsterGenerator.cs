using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public int FlowerNum = 3; 
    public float FlowerSpawnRange = 10.0f;
    public int GoblinNum = 3;
    public float GoblinSpawnRange = 10.0f;
    void generate(GameObject slime, GameObject flower, GameObject goblin)
    {
        for(int i = 0; i < SlimeNum; i++)
        {
            Instantiate(slime, new Vector3(Random.Range(-SlimeSpawnRange, SlimeSpawnRange), Random.Range(-SlimeSpawnRange,SlimeSpawnRange), 0), Quaternion.identity);
        }
        for(int i = 0; i < FlowerNum; i++)
        {
            Instantiate(flower, new Vector3(Random.Range(-FlowerSpawnRange, FlowerSpawnRange), Random.Range(-FlowerSpawnRange,FlowerSpawnRange), 0), Quaternion.identity);
        }
        for(int i = 0; i < GoblinNum; i++)
        {
            Instantiate(goblin, new Vector3(Random.Range(-GoblinSpawnRange, GoblinSpawnRange), Random.Range(-GoblinSpawnRange,GoblinSpawnRange), 0), Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generate(Resources.Load("Slime") as GameObject
                , Resources.Load("Flower") as GameObject
                , Resources.Load("Goblin") as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
