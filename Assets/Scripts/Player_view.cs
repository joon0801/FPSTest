using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player_view : MonoBehaviour
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
    private float CurrentCameraRotationX = 0f;

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
        // hide the cursor
        //Cursor.visible= false;
        //Cursor.lockState = CursorLockMode.Locked;
        // Move();
        CameraRotation();
        CharacterRotation();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ResetCameraRotationWithDelay(0.1f));
        }
    }

    /*
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkspeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    */

    /*CAMERA MOVEMENT Elevation-Axis*/
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * LookSensitivity;

        CurrentCameraRotationX -= _cameraRotationX;
        CurrentCameraRotationX = Mathf.Clamp(CurrentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(CurrentCameraRotationX, 0f, 0f);
    }
    /*CAMERA MOVEMENT Azimuth-Axis*/
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _CharacterRotationY = new Vector3(0f, _yRotation, 0f) * LookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_CharacterRotationY));
    }

    /* Coroutine to Reset Camera Rotation with Delay */
    private IEnumerator ResetCameraRotationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCameraRotation();
    }

    /* Reset Camera Rotation */
    private void ResetCameraRotation()
    {
        CurrentCameraRotationX = 0f; // Reset the current camera rotation X value
        theCamera.transform.localEulerAngles = Vector3.zero; // Reset the camera's local rotation to (0, 0, 0)
        transform.localEulerAngles = Vector3.zero; // Reset the character's rotation to (0, 0, 0)
    }
}