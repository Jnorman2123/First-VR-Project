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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // new input device variable set to XRNode inputSource
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        // get value of device axis input
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
}
