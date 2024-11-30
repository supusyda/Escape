using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent<PlayerBase> OnTouchDoor = new UnityEvent<PlayerBase>();
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
}
