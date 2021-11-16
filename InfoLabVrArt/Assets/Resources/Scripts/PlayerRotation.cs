using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private XRNode _inputSource;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = -9.81f;


    private Vector2 _inputAxis;
    private CharacterController _character;
    private XRRig _xrRig;

    void Start()
    {
        _character = GetComponent<CharacterController>();
        _xrRig = GetComponent<XRRig>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(_inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _inputAxis);
    }

    private void FixedUpdate()
    {
        _character.transform.Rotate(0, -_inputAxis.y * _speed, 0);
    }
}
