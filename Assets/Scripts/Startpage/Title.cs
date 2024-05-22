using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{    
    public void ClickStart()
    {
        Debug.Log("Loading MainGame... ");
        SceneManager.LoadScene(1);
    }

    public void ClickTuto()
    {
        Debug.Log("Loading Tutorial... ");
        SceneManager.LoadScene(3);
    }

    public void ClickCredits()
    {
        Debug.Log("Loading Credits... ");
        SceneManager.LoadScene(5);
    }

}
