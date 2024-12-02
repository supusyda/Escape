using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 StartPos;
    void Start()
    {
        StartPos = transform.position;
    }
    public void ResetTransformPos()
    {
        transform.position = StartPos;
    }
}
