using UnityEngine;

public class FireWallItem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem.Stop();
    }

    public void Play()
    {
        _particleSystem.Play();
    }

    public void Stop()
    {
        _particleSystem.Stop();
    }
}
