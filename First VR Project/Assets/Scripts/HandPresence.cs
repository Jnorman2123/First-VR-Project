using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // Declare new list of game objects
    public List<GameObject> controllerPrefabs;
    // Declare new list of devices
    private List<InputDevice> devices;
    // Declare new set of input device characteristics
    public InputDeviceCharacteristics controllerCharacteristics;
    // Declare targetDevice variable
    private InputDevice targetDevice;
    // Declare new game object variables
    private GameObject prefab;
    private GameObject spawnedController;
    // Start is called before the first frame update
    void Start()
    {
        // Set devices to new list of input devices
        devices = new List<InputDevice>();

        // Get devices with controller characteristics
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        // Loop through devices and debug log each device name and characteristics
        foreach(var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        // If devices is greater than zero set targetDevice to first device
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
            // Set prefab to controller if controller name is equal to targetDevice name
            prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            // If prefab is found set instantiate a new controller prefab and set it to spawnedController
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            // If prefab is not found log error and instantiate first controller prefab and set to spawnedController
            {
                Debug.Log("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get feature values of primary button, trigger, and d pad of target device
        
        
        

        // Check to see the feature value and if button is pressed and then log
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Debug.Log("Pressing Primary Button");
        }
        
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Pressing Trigger " + triggerValue);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }
    }
}
