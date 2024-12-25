using UnityEngine;
using UnityEngine.Tilemaps;

public class Target : MonoBehaviour, IHitParticle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public virtual void Hit(Vector2 contactPoint)
    {
        HitParticle(contactPoint);
    }
    public void HitParticle(Vector2 contactPoint)
    {

        ParticalSpawner.OnSpawnPartical.Invoke(Vector2.one, contactPoint, ParticleDefine.HIT_PARTICLE);
        AudioManager.Instance?.PlaySound(AudioClipName.SFX_HIT);
    }
}
