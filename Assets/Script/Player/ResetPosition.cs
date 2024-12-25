using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // [SerializeField] private Transform StartPos;
    private Vector2 startPos;

    void OnEnable()
    {
        startPos = transform.position;
        // ResetTransformPos(); 
    }
    public void ResetTransformPos()
    {
        transform.localScale = new Vector3(1, 1, 1);//fix the facing direction
        transform.position = startPos;
    }
}
