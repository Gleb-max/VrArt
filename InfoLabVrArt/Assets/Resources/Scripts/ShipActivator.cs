using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipActivator : MonoBehaviour
{
    [SerializeField] private ShipSceneStarter _shipSS;
    [SerializeField] private GameObject destroyWindowObject;
    [SerializeField] private Transform destroyPrefab;
    [SerializeField] private BreakableWindow windowPrefab;
    [SerializeField] private BreakableWindow _window;

    private Vector3 destroyPrefabSpawn;

    private bool isTriggered = false;

    private void Awake()
    {
        destroyPrefabSpawn = destroyWindowObject.transform.position;
        Destroy(destroyWindowObject);
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            if (isTriggered) return;
            _shipSS.Run();
            isTriggered = true;
            StartCoroutine(breakWindow());
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            _shipSS.Stop();
            isTriggered = false;
        }
    }

    IEnumerator breakWindow()
    {
        yield return new WaitForSeconds(1);
        if (isTriggered)
        {
            StartCoroutine(MakeNewWindow(_window.transform.position, _window.transform.rotation));
            Transform newPrefab = Instantiate(destroyPrefab);
            newPrefab.transform.position = destroyPrefabSpawn;
            newPrefab.GetComponent<Rigidbody>().AddForce(transform.right * 100);
        }
    }

    IEnumerator MakeNewWindow(Vector3 position, Quaternion rotation)
    {
        yield return new WaitWhile(() => !isTriggered);
        var newWindow = Instantiate(windowPrefab, position, rotation);
        Destroy(_window.gameObject);
        _window = newWindow;
    }
}
