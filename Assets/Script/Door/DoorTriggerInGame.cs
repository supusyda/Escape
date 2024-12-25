using System;
using System.Linq;
using UnityEngine;

public class DoorTriggerInGame : DoorTrigger
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] DoorSO doorOrder;
    [SerializeField] int doorIndex;

    void Start()
    {
        ResetDoor();
    }
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(ResetDoor);
    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(ResetDoor);
    }

    private void ResetDoor()
    {
        if (doorOrder.GetOpenDoors().Contains(doorIndex))
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(Tag.ANGEL) && doorOrder.GetOpenDoors().Contains(doorIndex))
        {

            OnTouchDoor.Invoke();
            other.GetComponent<PlayerBase>().playerRecord.ChangeState(RecordState.None);
        }
    }


}