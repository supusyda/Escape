using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Scriptable Objects/PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public float maxMoveSpeed = 1f;
    public float JumpHeight = 1.2f;
    public float accelerateSpeed = 5f;
    public float maxAccelerateSpeedTime = 1f;
    public float decelerateSpeedTime = .3f;
    public float fallGravityScale = 5;
    public float gravityScale = 1;



}
