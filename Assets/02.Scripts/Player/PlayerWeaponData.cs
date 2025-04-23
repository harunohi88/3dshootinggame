using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Scriptable Objects/PlayerWeaponData")]
public class PlayerWeaponData : ScriptableObject
{
    public int MaxBulletCount;
    public float BulletCoolTime;
    public float BulletReloadTime;
    public int BulletDamage;
    public float KnockBackDistance;
}
