using SteamAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SofaIndicator : MonoBehaviour
{

    
    public Text SOFA_Indicator;

    public SteamAudioManager SAM;
   
    // GameObject[] hrtf;


    // Start is called before the first frame update
    void Start()
    {
        SetSOFA();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
        {

        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {

        }

    }


    private void SetSOFA()
    {
        //string nowsofa = GetComponent<>();
        var hrtf = GetComponent<SteamAudioManager>().hrtfNames.ToString();
        //SOFA_Indicator.text = nowsofa;
        for (int i = 0; i <  hrtf.Length; i++)
        {     
            

        }



        //Debug.Log(nowsofa);




    }
}
