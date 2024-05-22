using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSceneChanger : MonoBehaviour
{
    public Score ScoreupGameObject;

    private void Update()
    {
        ScoreSum();
    }

    public void ScoreSum()
    {
        int CoScore = ScoreupGameObject.score;
        int MsScore = ScoreupGameObject.MissScore;

        int TotalScore = CoScore+ MsScore;

        if (TotalScore == 20)
        {
            Debug.Log("Scene Changed");
            StartCoroutine(changeSceneToScore());
        }

    }
    IEnumerator changeSceneToScore()
    {

        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(4);
    }
}
