using UnityEngine;

public class ParticleDestroy : ParticleBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem _ps;


    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void FixedUpdate()
    {
        if (_ps && !_ps.IsAlive())
        {
            base.DespawnObject();
        }
    }
}
