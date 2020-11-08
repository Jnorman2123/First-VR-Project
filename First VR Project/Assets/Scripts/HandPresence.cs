using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // Declare new list of devices
    private List<InputDevice> devices;
    // Start is called before the first frame update
    void Start()
    {
        // Set devices to new list of input devices and get devices
        devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        // Loop through devices and debug log each device name and characteristics
        foreach(var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
