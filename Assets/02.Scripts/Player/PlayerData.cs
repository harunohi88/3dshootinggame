using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float OriginalSpeed;
    public float DashSpeed;
    public float RollSpeed;
    public float ClimbSpeed;
    public float RollTime;
    public float RollStamina;
    public float ClimbStamina;
    public float JumpPower;
    public float MaxStamina;
    public float DashStamina;
    public float StaminaGainPerSecond;
    public float WallCheckDistance;
}
