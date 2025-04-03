using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Arrow : MonoBehaviour
{
    public float ArrowMovingSpeed = 5.0f;
    public float ArrowDestroyDistance = 10.0f;
    float ArrowATKDMG;
    int target;
    GameObject[] monster;
    GameObject arrow;
    GameObject player;

    void TrackMonster()
    {
        Vector2 MousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        monster = GameObject.FindGameObjectsWithTag("Slime").
                    Concat(GameObject.FindGameObjectsWithTag("Flower")).
                    Concat(GameObject.FindGameObjectsWithTag("Goblin")).ToArray();
        float[] angles = new float[monster.Length];
        for (int i = 0; i < monster.Length; i++)
        {
            angles[i] = Vector2.Angle(MousePos, monster[i].transform.position - transform.position);
        }
        float minAngle = angles.Min();
        for (int i = 0; i < monster.Length; i++)
        {
            if (angles[i] == minAngle)
            {
                target = i;
            }
        }
    }

    void Move()
    {
        transform.position += Vector3.Normalize(monster[target].transform.position - transform.position) * Time.deltaTime * ArrowMovingSpeed;
    }

    void HitSlime(Collider2D gameObject)
    {
        if (gameObject != null && 
        gameObject.CompareTag("Slime"))
        {
            gameObject.GetComponent<Slime>().TakeSlimeDMG(ArrowATKDMG);
            Destroy(arrow);
        }
    }
    //打花
    void HitFlower(Collider2D gameObject)
    {
        if (gameObject != null && 
        gameObject.CompareTag("Flower"))
        {
            gameObject.GetComponent<Flower>().TakeFlowerDMG(ArrowATKDMG);
            Destroy(arrow);
        }
    }
    void HitGoblin(Collider2D gameObject)
    {
        if (gameObject != null && 
        gameObject.CompareTag("Goblin"))
        {
            gameObject.GetComponent<Goblin>().TakeGoblinDMG(ArrowATKDMG);
            Destroy(arrow);
        }
    }
    void HitWall(Collider2D gameObject)
    {
        if (gameObject != null && 
        gameObject.CompareTag("Wall"))
        {
            Destroy(arrow);
        }
    }
    void Destroy()
    {
        //當箭矢與怪物的距離大於銷毀距離時，銷毀箭矢
        if (Vector3.Distance(player.transform.position, transform.position) > ArrowDestroyDistance)
        {
            Destroy(arrow);
        }
    }

    void Start()
    {
        ArrowATKDMG = Bow.BowATKDMG;
        arrow = gameObject;
        TrackMonster();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HitSlime(other);
        HitFlower(other);
        HitGoblin(other);
        HitWall(other);
        Destroy();
    }
}
