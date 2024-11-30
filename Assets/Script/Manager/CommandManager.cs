using UnityEngine;

public class CommandManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public bool startReplay = false;
    [SerializeField] public CommandScheduler[] playersRecord;

}
