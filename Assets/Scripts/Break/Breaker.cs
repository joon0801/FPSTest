using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{

    public JSON_tester targetGameObject;
    public GameObject panel;

    public 

    //private bool isPanelVisible = false; // Track the visibility state of the panel

    void Start()
    {
        // Check if targetGameObject is assigned
        if (targetGameObject == null)
        {
            Debug.LogError("TargetGameObject is not assigned.");
            return;
        }        
        panel.SetActive(false); // Ensure the panel starts hidden

    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바가 눌렸는지 확인
        {
            TogglePanel(); // 패널의 상태를 토글
        }
    }

    void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf); // 패널의 현재 상태를 반대로 변경

    }
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check for spacebar press
        {
            HandleSpacebarPress();
        }
        
    }

    void HandleSpacebarPress()
    {
        if ((targetGameObject.CurrentIndex == 10 || targetGameObject.CurrentIndex == 200 || targetGameObject.CurrentIndex == 300) && !isPanelVisible)
        {
            panel.SetActive(true); // Show the panel if CurrentIndex is 10, 200, or 300 and the panel is not already visible
            isPanelVisible = true; // Update the visibility state
        }

        panel.SetActive(false); // Hide the panel
        isPanelVisible = false; // Update the visibility state
        
    }
    */

}