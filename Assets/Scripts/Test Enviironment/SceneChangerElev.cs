using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerElev : MonoBehaviour
{
    public Score ScoreupGameObject;
    
    private void Update()
    {
        ScoreSum();
    }

    public void ScoreSum()
    {
        int CoScore = ScoreupGameObject.score;
        
        if (CoScore == 5)
        {
            Debug.Log("Scene Changed");
            StartCoroutine(changeSceneToAzim());
        }

    }
    IEnumerator changeSceneToAzim()
    {

        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(2);
    }

}
