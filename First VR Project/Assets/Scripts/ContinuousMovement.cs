using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    // Variable for input source for movement and input axis vector2
    public XRNode inputSource;
    private Vector2 inputAxis;
    // character variable and speed variable
    private CharacterController character;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // set character variable
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // new input device variable set to XRNode inputSource
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        // get value of device axis input
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);
    }
}
