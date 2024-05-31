using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public int score = 0;
    public int maxScore;

 

    public GameObject ScoreResult;

    private JSON_tester jsonParser;
    // Start is called before the first frame update
    void Start()
    {
        jsonParser = FindObjectOfType<JSON_tester>();
        score = jsonParser.CurrentIndex;
  
    }
    void Update()
    {
        UpdateScore();
        if (score == maxScore)
        {
            ScoreResult.SetActive(true);
        }
        else
        {
            ScoreResult.SetActive(false);
        }


    }
   
    public void UpdateScore()
    {
        ScoreText.text = "Score 0" + score;
       
    }
    


 
}

