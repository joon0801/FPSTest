using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreResultMenu : MonoBehaviour
{
    public GameObject ScoreUI;
    public Score ScoreupGameObject;
    public Text ScoreText;
    public Text MissScoreText;
    public Text Accuracy;
    


    // Update is called once per frame
    void Update()
    {       
        ScoreResult();
        
    }

    public void ScoreResult()
    {

        
        int CoScore = ScoreupGameObject.score;
        int MsScore = ScoreupGameObject.MissScore;
        int TotalScore = CoScore + MsScore; 

        



       
        if(TotalScore == 20)
        {
            ScoreUI.SetActive(true);
            Time.timeScale = 0f;
            
            ScoreText.text = CoScore.ToString();
            MissScoreText.text = MsScore.ToString();

            int resultAccuracy = CoScore*5;
            Accuracy.text = resultAccuracy.ToString() + "%";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
        else
        {
            ScoreUI.SetActive(false);
        }
        
    }

    public void clickMainMenu()
    {
        Debug.Log("Scene Changes To Main Menu");
        SceneManager.LoadScene(0);
    }
}
