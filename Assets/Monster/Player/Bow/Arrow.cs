using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Arrow : MonoBehaviour
{
    float ArrowMoveSpd = 5.0f;
    float ArrowDestroyD = 10.0f;
    int target;
    GameObject[] monster = new GameObject[0];
    GameObject arrow;
    GameObject player;

    void TrackMonster()
    {
        Vector2 MousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        foreach(string name in Player.MonsterName)
        {
            monster = monster.Concat(GameObject.FindGameObjectsWithTag(name)).ToArray();
        }
        if (monster.Length != 0)
        {
            float[] angles = new float[monster.Length];
            for (int i = 0; i < monster.Length; i++)
            {
                angles[i] = Vector2.Angle(MousePos, monster[i].transform.position - transform.position);
            }
            float minAngle = angles.Min();
            target = System.Array.IndexOf(angles, minAngle);
        }
    }

    void Move()
    {
        if (monster.Length != 0)
        {
            if (monster[target] != null)
            {
                transform.position += Vector3.Normalize(monster[target].transform.position - transform.position) * Time.deltaTime * ArrowMoveSpd;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Hit(Collider2D gameObject)
    {
        foreach (string name in Player.MonsterName.Concat(new string[] { "Wall" }))
        {
            if (gameObject != null && 
            gameObject.CompareTag(name))
            {
                switch (name)
                {
                    case "Slime":
                        gameObject.GetComponent<Slime>().TakeSlimeDMG(Bow.BowAtkDmg);
                        gameObject.GetComponent<Slime>().SlimePauseAttack = true;
                        break;
                    case "Flower":
                        gameObject.GetComponent<Flower>().TakeFlowerDMG(Bow.BowAtkDmg);
                        break;
                    case "Goblin":
                        gameObject.GetComponent<Goblin>().TakeGoblinDMG(Bow.BowAtkDmg);
                        break;
                    case "Wall":
                        break;
                }
                Destroy(arrow);
            }
        }
    }
    void Destroy()
    {
        //當箭矢與怪物的距離大於銷毀距離時，銷毀箭矢
        if (Vector3.Distance(player.transform.position, transform.position) > ArrowDestroyD)
        {
            Destroy(arrow);
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        arrow = gameObject;
        TrackMonster();
    }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
        Destroy();
    }
}
