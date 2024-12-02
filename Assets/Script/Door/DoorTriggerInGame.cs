using UnityEngine;

public class DoorTriggerInGame : DoorTrigger
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int doorIndex { get; private set; }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag.ANGEL))
            OnTouchDoor.Invoke();
    }
}
