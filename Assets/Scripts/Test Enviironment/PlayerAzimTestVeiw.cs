using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class PlayerAzimTestVeiw : MonoBehaviour
{
    private Rigidbody myRigid;

    [SerializeField]
    private float LookSensitivity;

    /*
    [SerializeField]
    private float walkspeed;
    */

    [SerializeField]
    private float CameraRotationLimit;
    //private float CurrentCameraRotationX = 0f;

    [SerializeField]
    private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        myRigid = GetComponent<Rigidbody>();
        theCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // CameraRotation();
        CharacterRotation();
    }

    //private void CameraRotation()
    //{

    //    float _xRotation = Input.GetAxisRaw("Mouse Y");
    //    float _cameraRotationX = _xRotation * LookSensitivity;

    //    CurrentCameraRotationX -= _cameraRotationX;
    //    CurrentCameraRotationX = Mathf.Clamp(CurrentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);

    //    theCamera.transform.localEulerAngles = new Vector3(CurrentCameraRotationX, 0f, 0f);

    //}
    /*CAMERA MOVEMENT Azimuth-Axis*/

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _CharacterRotationY = new Vector3(0f, _yRotation, 0f) * LookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_CharacterRotationY));

    }

}
