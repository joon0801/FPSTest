using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPlayMusic : MonoBehaviour
{
    public AudioClip musicA;
    public AudioClip musicB;
    public AudioSource audioSource;
    

    public void Stimuli_1()
    {
        audioSource.clip = musicA;
        audioSource.Play();
    }

    public void Stimuli_2()
    {
        audioSource.clip = musicB;
        audioSource.Play();
    }

    public void NextStep()
    {
        SceneManager.LoadScene(2);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(3);
    }

}
