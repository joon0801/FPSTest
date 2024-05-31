using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewResetter : MonoBehaviour
{

    public GameObject Player;
    //public GameObject Playerview;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ResetCameraRotationWithDelay(0.1f));

        }
    }
    void ResetRotation()
    {        
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);    

    }

    private IEnumerator ResetCameraRotationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetRotation();
    }

}
