using UnityEngine;

public class CommandManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public bool startReplay = false;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startReplay == true)
        {
            CommandScheduler.ExecuteReplay();
        }
    }
}
