using UnityEngine;

public class CharacterHealth : Health, IDamageAble
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }
}
