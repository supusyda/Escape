using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : Health, IDamageAble
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerBase _playerBase;
    public static UnityEvent<PlayerBase> OnCharacterHealthDied = new();
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(OnRestThisRound);
    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(OnRestThisRound);
    }
    private void OnRestThisRound()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }
    protected override void Die()
    {
        _playerBase = transform.parent.GetComponent<PlayerBase>();
        _playerBase.playerRecord.ChangeState(RecordState.None);
        base.Die();
        OnCharacterHealthDied.Invoke(_playerBase);

    }
}
