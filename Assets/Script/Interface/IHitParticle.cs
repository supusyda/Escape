using UnityEngine;

public interface IHitParticle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void HitParticle(Vector2 contactPoint);

}
