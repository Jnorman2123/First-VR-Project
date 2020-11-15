using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // New boolean variable to determine if controller should be shown 
    public bool showController = false;
    // Declare new list of game objects and new hand prefab object
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    // Declare new list of devices
    private List<InputDevice> devices;
    // Declare new set of input device characteristics
    public InputDeviceCharacteristics controllerCharacteristics;
    // Declare targetDevice variable
    private InputDevice targetDevice;
    // Declare new game object variables
    private GameObject prefab;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    // Declare new animator variable
    private Animator handAnimator;

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
            // Spawn a hand model prefab and set the hand animator variable
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If show controller is true display a controller otherwise show hand model
        if (showController)
        {
            // Deactivate hand model and activate controller
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            // Deactivate controller model and activate hand model
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
            // Call the update hand animation function
            UpdateHandAnimation();
        }
    }

    // Create new function to update the hand animator when grip or trigger is pressed
    void UpdateHandAnimation()
    {
        // If trigger is pressed set the hand animator trigger value to current value else set to zero
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        } 
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        // If grip is pressed set the hand animator grip value to current value else set to zero
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
