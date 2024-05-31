using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    public JSON_tester jsonParser; // JsonParser 스크립트 참조
    public Text Totalnumber;
    public Text CurrentTrial;

    void Start()
    {
        if (jsonParser == null || Totalnumber == null)// || CurrentTrial == null)
        {
            Debug.LogError("JsonParser or ProgressText is not assigned.");
            return;
        }

        UpdateProgressText();
    }

    void Update()
    {
        UpdateProgressText();
    }

    void UpdateProgressText()
    {
        Totalnumber.text = $"{jsonParser.DataItemCount}";
        //CurrentTrial.text = $"{jsonParser.CurrentIndex}";
    }
}
