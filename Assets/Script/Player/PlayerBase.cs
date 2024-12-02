using System;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rigidbody2D { get; private set; }
    public GroundCheck groundCheck { get; private set; }
    public PlayerRecord playerRecord { get; private set; }
    public PlayerMoveBase playerMoveBase { get; private set; }
    public ResetPosition resetPosition { get; private set; }
    [SerializeField] private bool isDemon = false;
    static public UnityEvent<PlayerBase, bool> LoadPlayer = new UnityEvent<PlayerBase, bool>();


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
        resetPosition = GetComponent<ResetPosition>();
        EventAddLis();
    }
    void Start()
    {
        LoadPlayer.Invoke(this, isDemon);
    }
    void EventAddLis()
    {
        DoorTriggerInGame.OnTouchDoor.AddListener(OnTouchDoor);
    }
    void EventRemoveLis()
    {
        DoorTriggerInGame.OnTouchDoor.RemoveListener(OnTouchDoor);
    }

    private void OnTouchDoor()
    {
        resetPosition.ResetTransformPos();
    }
}
