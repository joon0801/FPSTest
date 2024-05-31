using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteamAudio;

public class SOFAChanger : MonoBehaviour
{
    // Array to store different HRTF file names or paths
    /*
    public string[] hrtfNames = {
        "Assets/HRTF/Default.sofa",
        "Assets/HRTF/HRTF1.sofa",
        "Assets/HRTF/HRTF2.sofa"
    };
    */
    private SteamAudioManager steamAudioManager;

    void Start()
    {
        // Get the SteamAudioManager component
        steamAudioManager = FindObjectOfType<SteamAudioManager>();
        if (steamAudioManager == null)
        {
            Debug.LogError("SteamAudioManager component not found!");
        }
    }

    void Update()
    {
        // Check for key inputs and change HRTF accordingly
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeHRTFByIndex(0); // Keypad 1 corresponds to index 0
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeHRTFByIndex(1); // Keypad 2 corresponds to index 1
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeHRTFByIndex(2); // Keypad 3 corresponds to index 2
        }
    }

    void ChangeHRTFByIndex(int index)
    {
        // Make sure the index is within the bounds of the array
        if (index >= 0)// && index < hrtfNames.Length)
        {
            if (steamAudioManager != null)
            {
                // Update the HRTF using a hypothetical method, replace with actual method
                steamAudioManager.currentHRTF = index;
                Debug.Log("Current HRTF changed ");
            }
        }
        else
        {
            Debug.LogError("Invalid HRTF index!");
        }
    }
}
