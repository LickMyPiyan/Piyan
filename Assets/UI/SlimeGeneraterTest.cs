using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SlimeGeneraterTest : MonoBehaviour
{
    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public GameObject MobHealth;

    public IEnumerator SpawnMob(string MobName, int Num, float MobSpawnRange)
    {
        for (int i = 0; i < Num; i++)
        {
            GameObject Mob = Instantiate(Resources.Load(MobName), new Vector3(Random.Range(-MobSpawnRange, MobSpawnRange), Random.Range(-MobSpawnRange, MobSpawnRange), 0), Quaternion.identity) as GameObject;
            GameObject healthBar = Instantiate(Resources.Load("MobHealth"), Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(GameObject.Find("MobHealthBars").transform);

            HealthBar MobHealth = healthBar.GetComponent<HealthBar>();
            MobHealth.target = Mob.transform;
            MobHealth.offset = new Vector3(0, -1, 0);
        }
        yield return null;
    }

    void Start()
    {
        StartCoroutine(SpawnMob("Slime", SlimeNum, SlimeSpawnRange));
    }

    void Update()
    {
        
    }
}
