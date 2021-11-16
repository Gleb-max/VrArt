using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
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
        Quaternion headYaw = Quaternion.Euler(0, _xrRig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(_inputAxis.x, 0, _inputAxis.y);

        _character.Move(direction * _speed * Time.fixedDeltaTime);
        _character.Move(Vector3.up * _gravity * Time.fixedDeltaTime);
    }
}
