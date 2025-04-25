using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveStats", menuName = "Scriptable Objects/PlayerMoveStats")]
public class PlayerMoveStats : ScriptableObject
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
