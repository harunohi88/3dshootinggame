using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Scriptable Objects/PlayerWeaponData")]
public class PlayerWeaponData : ScriptableObject
{
    public float BombSpeed;
    public int MaxBombCount;
    public float MaxBombChargeTime;
    public int MaxBulletCount;
    public float BulletCoolTime;
    public float BulletReloadTime;
}
