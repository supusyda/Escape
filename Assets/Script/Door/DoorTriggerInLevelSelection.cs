using UnityEngine;

public class DoorTriggerInLevelSelection : DoorTrigger
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int doorLevelIndex;
    void Start()
    {

        if (LevelIsUnlocked())
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();

        }
    }
    bool LevelIsUnlocked()
    {
        return LevelManager.Instance.LoadCurrentLevelFormPlayerPref() >= doorLevelIndex;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelIsUnlocked())
        {
            GetComponent<Scale>().BeginScale();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!LevelIsUnlocked()) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Enter Level " + doorLevelIndex);
            LevelManager.Instance.OnLevelSelect(doorLevelIndex);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Scale>().EndScale();
    }
}
