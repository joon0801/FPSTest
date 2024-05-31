using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SceneInitial()
    {
        SceneManager.LoadScene(0);
    }   
    public void SceneIntro1()
    {
        SceneManager.LoadScene(1);
    }
    public void SceneIntro2()
    {
        SceneManager.LoadScene(2);
    }
    public void SceneIntro3()
    {
        SceneManager.LoadScene(3);
    }
    public void SceneIntro4()
    {
        SceneManager.LoadScene(4);
    }
    public void SceneIntro5()
    {
        SceneManager.LoadScene(5);
    }
    public void SceneIntro6()
    {
        SceneManager.LoadScene(6);
    }
    public void SceneVolumeCali()
    {
        SceneManager.LoadScene(7);
    }
    public void SceneTutorial()
    {
        SceneManager.LoadScene(8);
    }
    public void SceneMainGame()
    {
        SceneManager.LoadScene(9);
    }


}
