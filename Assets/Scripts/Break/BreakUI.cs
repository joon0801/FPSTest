using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


public class BreakUI : MonoBehaviour
{

    public JSON_tester TargetGameObject;
    public GameObject Break;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (TargetGameObject == null)
        {
            Debug.LogError("JsonParser or ProgressText is not assigned.");
            return;
        }               
    }

    void Update()
    {
        i = TargetGameObject.CurrentIndex;
            

        if (i == 10 || i == 200 || i == 300)
        {
            Break.SetActive(true);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Break.SetActive(false);
            Debug.Log("space");
        }

    }


}
