using System;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent<Vector2, Vector2, string, string> ShootBullet = new();//bullet dir , position , prefab name,shooter tag
    public static UnityEvent shootFired = new();
    public Vector2 shootDir { get; private set; } = Vector2.zero;
    private float coolDown = 1f;
    private float coolDownTimer = 0;
    private StarterAssetsInputs _playerInput;
    private PlayerBase _playerBase;
    private bool _canShoot = false;

    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(OnRestThisRound);
        GameManager.OnHasInputActive.AddListener(OnHasInputActive);
    }

    private void OnHasInputActive()
    {
        _canShoot = true;
        coolDownTimer = coolDown;

    }

    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(OnRestThisRound);
        GameManager.OnHasInputActive.RemoveListener(OnHasInputActive);
    }
    private void OnRestThisRound()
    {
        coolDownTimer = coolDown;
        _playerInput.shoot = false;
    }

    void Start()

    {
        _playerBase = GetComponent<PlayerBase>();
        _playerInput = GetComponent<StarterAssetsInputs>();
        shootDir = _playerBase.playerMoveBase.lookDir;
        coolDownTimer = coolDown;
    }


    public virtual void Shoot()
    {
        Vector3 spawnDir = new Vector3(transform.localScale.x, 0, 0);
        ShootBullet.Invoke(spawnDir, transform.position, BulletName.BULLET_NAME, transform.tag);
        coolDownTimer = 0;
        _playerInput.shoot = false;


        // m_GunFired.Invoke();
    }
    void FixedUpdate()
    {
        if (coolDownTimer < coolDown)
        {
            coolDownTimer += Time.fixedDeltaTime;
            return;
        }

        if (!_canShoot) return;
        if (!_playerInput.shoot) return;//player input
        shootDir = _playerBase.playerMoveBase.lookDir;//set bullet move dir
        _playerBase.playerRecord.AddRecord(new InputCommandShoot(this));




    }
}
