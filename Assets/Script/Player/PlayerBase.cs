using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rigidbody2D { get; private set; }
    public GroundCheck groundCheck { get; private set; }
    public PlayerRecord playerRecord { get; private set; }
    public PlayerMoveBase playerMoveBase { get; private set; }


    protected virtual void OnEnable()
    {
        LoadComponent();
    }
    void LoadComponent()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck").GetComponent<GroundCheck>();
        playerRecord = GetComponent<PlayerRecord>();
        playerMoveBase = GetComponent<PlayerMoveBase>();


    }
}
