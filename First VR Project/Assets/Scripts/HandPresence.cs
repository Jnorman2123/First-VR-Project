using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // Declare new list of devices
    private List<InputDevice> devices;
    // Declare new set of input device characteristics
    private InputDeviceCharacteristics rightControllerCharacteristics;
    // Declare targetDevice variable
    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        // Set devices to new list of input devices and rightControllerCharacteristics
        devices = new List<InputDevice>();
        rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        // Get devices with right controller characteristics
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        // Loop through devices and debug log each device name and characteristics
        foreach(var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        // If devices is greater than zero set targetDevice to first device
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get feature values of primary button, trigger, and d pad of target device
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);

        // Check to see if button is pressed and log according to each button
        if (primaryButtonValue)
        {
            Debug.Log("Pressing Primary Button");
        }
        
        if (triggerValue > 0.1f)
        {
            Debug.Log("Pressing Trigger " + triggerValue);
        }
        if (primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }
    }
}
