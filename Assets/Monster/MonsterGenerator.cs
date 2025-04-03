using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public int FlowerNum = 3; 
    public float FlowerSpawnRange = 10.0f;
    public int GoblinNum = 3;
    public float GoblinSpawnRange = 10.0f;
    public IEnumerator SpawnMob(string MobName, int Num, float MobSpawnRange)
    {
        for (int i = 0; i < Num; i++)
        {
            GameObject Mob = Instantiate(Resources.Load(MobName), new Vector3(Random.Range(-MobSpawnRange, MobSpawnRange), Random.Range(-MobSpawnRange, MobSpawnRange), 0), Quaternion.identity) as GameObject;
            GameObject healthBar = Instantiate(Resources.Load("MobHealth"), Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(GameObject.Find("MobHealthBars").transform);

            HealthBar MobHealth = healthBar.GetComponent<HealthBar>();
            MobHealth.target = Mob;
            MobHealth.offset = new Vector3(0, -1, 0);
        }
        yield return null;
    }

    void genarate()
    {
        StartCoroutine(SpawnMob("Slime", SlimeNum, SlimeSpawnRange));
        StartCoroutine(SpawnMob("Flower", FlowerNum, FlowerSpawnRange));
        StartCoroutine(SpawnMob("Goblin", GoblinNum, GoblinSpawnRange));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        genarate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
