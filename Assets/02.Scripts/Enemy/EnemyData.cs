using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float FindDistance = 7f;
    public float ReturnDistance = 10f;
    public float MoveSpeed = 3f;
    public float AttackDistance = 2f;
    public float MinMoveDistance = 0.1f;
    public float AttackCooltime = 2f;
    public float MaxHealth = 100f;
    public float DamagedTime = 0.5f;
    public float DieTime = 2f;
    public float PatrolConversionTime = 3f;
    public float PatrolDistance = 6f;
}
