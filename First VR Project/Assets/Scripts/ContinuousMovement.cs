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
    // character, rig, and speed variables
    private CharacterController character;
    private XRRig rig;
    public float speed = 1.0f;
    // gravity and fallingSpeed variables
    public float gravity = 9.81f;
    public float fallingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // set character and rig variables
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
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
        // new quaternion for head rotation
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        //  new Vector3 for character movement based on user input
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);
        // set fallingSpeed
        fallingSpeed = -10;
        // player fall by fallingSpeed
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
}
