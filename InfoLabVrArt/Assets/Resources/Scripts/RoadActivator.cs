using System.Collections;
using System.Threading;
using UnityEngine;

public class RoadActivator : MonoBehaviour
{
    [SerializeField] private float _activationSpeed;

    private Material _roadMaterial;

    private int _onShowingRoad;
    private int _onHidingRoad;

    private void Start()
    {
        var road = GetComponentInChildren<Road>();
        _roadMaterial = road.GetComponent<MeshRenderer>().materials[0];
        _roadMaterial.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            var isShowing = Interlocked.Exchange(ref _onShowingRoad, 1);
            if(isShowing == 1) return;
            while (Volatile.Read(ref _onHidingRoad) == 1) { }

            StartCoroutine(SmoothShowRoad());

            Interlocked.Exchange(ref _onShowingRoad, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            var isHiding = Interlocked.Exchange(ref _onHidingRoad, 1);
            if(isHiding == 1) return;
            while (Volatile.Read(ref _onShowingRoad) == 1) { }

            StartCoroutine(SmoothHideRoad());

            Interlocked.Exchange(ref _onHidingRoad, 0);
        }
    }

    private IEnumerator SmoothShowRoad()
    {
        for (float i = 0.0f; i <= 1.0f; i += 0.05f)
        {
            yield return new WaitForSeconds(1 / _activationSpeed);
            _roadMaterial.color = new Color(1, 1, 1, i);
        }
        _roadMaterial.color = new Color(1, 1, 1, 1);

        yield return null;
    }

    private IEnumerator SmoothHideRoad()
    {
        for (float i = 1.0f; i >= 0.0f; i -= 0.05f)
        {
            yield return new WaitForSeconds(1 / _activationSpeed);
            _roadMaterial.color = new Color(1, 1, 1, i);
        }
        _roadMaterial.color = new Color(1, 1, 1, 0);

        yield return null;
    }
}
