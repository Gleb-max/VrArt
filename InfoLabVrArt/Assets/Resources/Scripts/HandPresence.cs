using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandPresence : MonoBehaviour
{
    [SerializeField] private InputDeviceCharacteristics _controllerCharacteristics;
    [SerializeField] private GameObject _controllerPrefab;

    private InputDevice _targetController;
    private GameObject _spawnedController;
    private Animator _handAnimator;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(_controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            _targetController = devices[0];
            _spawnedController = Instantiate(_controllerPrefab, transform);
            _handAnimator = _spawnedController.GetComponent<Animator>();
        }
    }

    void Update()
    {
       // UpdateHandAnimation();
    }

    private void UpdateHandAnimation()
    {
        if (_targetController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            _handAnimator.SetFloat("Trigger", triggerValue);
        else
            _handAnimator.SetFloat("Trigger", 0.0f);

        if (_targetController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            _handAnimator.SetFloat("Grip", gripValue);
        else
            _handAnimator.SetFloat("Grip", 0.0f);
    }
}
