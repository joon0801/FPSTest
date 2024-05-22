using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCursor : MonoBehaviour
{
    [SerializeField]
    private Camera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
