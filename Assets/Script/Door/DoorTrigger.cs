using UnityEngine;
using UnityEngine.Events;
public enum DoorState { None, Open, Close, Lock }

public class DoorTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent OnTouchDoor = new UnityEvent();
    private DoorState currentState;
    [SerializeField] Transform openDoorModel;
    [SerializeField] Transform closeDoorModel;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
    public virtual void OpenDoor()
    {
        if (currentState != DoorState.Lock)
        {
            currentState = DoorState.Open;
            // Add logic to open the door
            openDoorModel.gameObject.SetActive(true);
            closeDoorModel.gameObject.SetActive(false);
        }
    }

    public virtual void CloseDoor()
    {
        if (currentState != DoorState.Lock)
        {
            currentState = DoorState.Close;
            // Add logic to close the door
            openDoorModel.gameObject.SetActive(false);
            closeDoorModel.gameObject.SetActive(true);
        }
    }
}
