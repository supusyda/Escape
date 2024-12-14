using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform StartPos;
    void Start()
    {
        // StartPos = transform.position;
    }
    public void ResetTransformPos()
    {
        transform.position = StartPos.position;
    }
}
