using UnityEngine;

public class ActiveObject : Target
{
    override public void Hit(Vector2 contactPoint)
    {
        Debug.Log("ASSSSSSS");
        base.Hit(contactPoint);
        transform.parent.GetComponent<ActiveDisapearPlatform>().DespawnPlatform();
        gameObject.SetActive(false);
    }

}
