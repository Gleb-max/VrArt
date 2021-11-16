using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireWallActivator : MonoBehaviour
{
    private IEnumerable<FireWallItem> _firewalls;

    [SerializeField] 
    private AudioSource _fireWallSounds;

    private bool isFirewallShowing = false;

    private FireWallItem showingNow;

    void Start()
    {
        _firewalls = gameObject.GetComponentsInChildren<FireWallItem>();
        updateShowingItem();
        StartCoroutine(startFireWall());
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            _fireWallSounds.Play();
            isFirewallShowing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            _fireWallSounds.Stop();
            isFirewallShowing = false;
        }
    }

    void updateShowingItem()
    {
        int index = new System.Random().Next(0, _firewalls.Count());
        showingNow = _firewalls.ElementAt(index);
    }

    IEnumerator startFireWall()
    {
        while (true)
        {
            if (isFirewallShowing)
            {
                showingNow.Play();
                yield return new WaitForSeconds(1f);
                showingNow.Stop();
                updateShowingItem();
            }
            else yield return new WaitWhile(() => isFirewallShowing);
        }
    }
}
