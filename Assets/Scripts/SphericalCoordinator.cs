using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalCoordinator : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Rotation speed

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the ball around the X-axis when moving the mouse up and down
        transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime);

        // Rotate the ball around the Y-axis when moving the mouse left and right
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
    }
}
