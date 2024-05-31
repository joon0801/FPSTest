using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.FullSerializer;

public class ScoreResultMenu : MonoBehaviour
{
    public GameObject ScoreUI;
    //public Score ScoreupGameObject;
    
  

    private JSON_tester jsonNumber;


    void Start()
    {
        // JsonParser 인스턴스를 찾음
        jsonNumber = FindObjectOfType<JSON_tester>();

        if (jsonNumber = null)
        {
            
            Debug.LogError("JSONtester not found in the scene.");
        }
       
    }

    // Update is called once per frame
    void Update()
    {       
        ScoreResult();
        
    }

    public void ScoreResult()
    {

        /*
        int CoScore = ScoreupGameObject.score;
        int MsScore = ScoreupGameObject.MissScore;
        int TotalScore = CoScore + MsScore; 
        */



        int count = jsonNumber.DataItemCount;




        if (jsonNumber != null && jsonNumber.CurrentIndex >= jsonNumber.DataItemCount)
        {
            ScoreUI.SetActive(true);
            Time.timeScale = 0f;
            
            //ScoreText.text = CoScore.ToString();
            //MissScoreText.text = MsScore.ToString();
            
            //int resultAccuracy = CoScore*5;
            //Accuracy.text = resultAccuracy.ToString() + "%";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("Experiment Done!!");
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
