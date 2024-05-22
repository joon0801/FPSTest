using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public int score = 0;
    public int maxScore;
    public Text MissScoreText;
    public int MissScore = 0;
    public int MissmaxScore;
    public int SumScore;
    public GameObject Scored;
    public GameObject YouText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        MissScore = 0;
    }

    public void Addscore(int newScore)
    {
        score += newScore;
        if (score >= maxScore) score = maxScore;
        
    }
    public void Miss(int newMissScore)
    {
        MissScore += newMissScore;
        if (MissScore >= MissmaxScore) MissScore = MissmaxScore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score 0" + score;
       
    }
    public void UpdateMiss()
    {       
        MissScoreText.text = "Miss 0" + MissScore;
    }


    void Update()
    {
        UpdateScore();
        //if (score == maxScore)
        {
        //    Scored.SetActive(false);
        }
        UpdateMiss();
        //if (MissScore == MissmaxScore)
        {
          //  Scored.SetActive(false);
        }

        

    }
}

