using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSceneStarter : MonoBehaviour
{
    [SerializeField] private GameObject _water;
    [SerializeField] private AudioSource _waterSounds;
    [SerializeField] private Shoot _shot;

    void Awake()
    {
        Stop();
    }

    void Update()
    {
        
    }

    public void Run()
    {
        _water.GetComponent<Water>().enabled = true;
        _waterSounds.Play();
        _shot.Shot();
    }

    public void Stop()
    {
        _water.GetComponent<Water>().enabled = false;
        _waterSounds.Stop();
    }
}
