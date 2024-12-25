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
    public PlayerUI playerUI;
    public CharacterHealth playerHealth { get; private set; }


    public PlayerAnimationController playerAnim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    public ResetPosition resetPosition { get; private set; }
    public Shooting shooting { get; private set; }
    public Transform footPosition { get; private set; }
    public Collider2D playerCollider { get; private set; }
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
        playerUI = transform.Find("PlayerUI")?.GetComponent<PlayerUI>();
        footPosition = transform.Find("FootPosition")?.transform;
        playerHealth = transform.Find("Health")?.GetComponent<CharacterHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();


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

    }
    void EventRemoveLis()
    {
        // DoorTriggerInGame.OnTouchDoor.RemoveListener(OnTouchDoor);
        GameManager.OnResetScene.RemoveListener(OnTouchDoor);

    }

    private void OnTouchDoor()
    {
        resetPosition.ResetTransformPos();
    }
    public void DeactivePlayer()
    {
        Debug.Log("Deactive" + transform.name);
        gameObject.layer = LayerMask.NameToLayer("None");
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

    }
    public void Active()
    {

        Debug.Log("Active" + transform.name);
        gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;

    }
}
