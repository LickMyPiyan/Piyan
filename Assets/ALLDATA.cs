using UnityEngine;

[CreateAssetMenu(fileName = "ALLDATA", menuName = "Scriptable Objects/ALLDATA")]
public class ALLDATA : ScriptableObject
{
    public float PlayerSpeed = 1.0f;
    public float DashDistance = 3.0f;
    public float DashCooldown = 3.0f;


    public int SlimeNum = 3;
    public float SlimeSpawnRange = 5;
    public int SlimeHealth = 100;
    public float SlimeMovimgSpeed = 1.0f;
    public float SlimeJumpForce = 1.0f;
    public float SlimeJumpCD = 1.0f;
    public float SlimeTrackRange = 1.0f;


    public float AttackCD = 0.5f;
    public int Damage = 100;

}
