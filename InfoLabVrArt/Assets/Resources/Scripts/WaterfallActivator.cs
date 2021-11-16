using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterfallActivator : MonoBehaviour
{
    [SerializeField] private AudioSource _waterfallSounds;

    private IEnumerable<Waterfall> _waterfalls;
    
    void Start()
    {
        _waterfalls = gameObject.GetComponentsInChildren<Waterfall>();
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            _waterfallSounds.Play();
            foreach(Waterfall waterfall in _waterfalls)
            {
                waterfall.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            foreach (Waterfall waterfall in _waterfalls)
            {
                waterfall.Stop();
            }
            _waterfallSounds.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
