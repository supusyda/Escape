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
    public WallCheck wallCheck { get; private set; }

    public PlayerAnimationController playerAnim { get; private set; }

    public ResetPosition resetPosition { get; private set; }
    public Shooting shooting { get; private set; }
    public Transform footPosition { get; private set; }
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
        shooting = GetComponent<Shooting>();
        playerAnim = GetComponent<PlayerAnimationController>();
        wallCheck = transform.Find("WallCheck")?.GetComponent<WallCheck>();
        footPosition = transform.Find("FootPosition")?.transform;

        EventAddLis();
    }
    void OnDisable()
    {
        EventRemoveLis();
    }
    void Start()
    {
        LoadPlayer.Invoke(this, isDemon);
    }
    void EventAddLis()
    {
        // DoorTriggerInGame.OnTouchDoor.AddListener(OnTouchDoor);
        GameManager.OnResetScene.AddListener(OnTouchDoor);
        GameManager.OnEndTurn.AddListener(OnTouchDoor);
    }
    void EventRemoveLis()
    {
        // DoorTriggerInGame.OnTouchDoor.RemoveListener(OnTouchDoor);
        GameManager.OnResetScene.RemoveListener(OnTouchDoor);
        GameManager.OnEndTurn.RemoveListener(OnTouchDoor);
    }

    private void OnTouchDoor()
    {
        resetPosition.ResetTransformPos();
    }
}
