using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SteamAudio;

public class TutorialGameSoundPlayer : MonoBehaviour
{
    public GameObject[] TargetsToHit;
    public GameObject DoneUI;
    public Text Trial;
    public AudioClip[] Stimuli;

    private SteamAudioManager steamAudioManager;
    private int clickNum = 0;
    private int trialNum = 0;
    private int currentClipIndex = 0;
    private bool isFirstRun = true; // Track if it's the first run

    void Start()
    {
        steamAudioManager = FindObjectOfType<SteamAudioManager>();
        SOFASelector();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SOFASelector();
            Counter();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSoundClip();
        }
    }

    void SOFASelector()
    {
        int hrtfIndex = Random.Range(0, 3);
        steamAudioManager.currentHRTF = hrtfIndex;
        Debug.Log("Current SOFA: " + hrtfIndex);

        if (!isFirstRun)
        {
            clickNum++;
        }
        else
        {
            isFirstRun = false; // Mark the first run as done
        }

        if (clickNum >= TargetsToHit.Length)
        {
            clickNum = 0; // Reset to start if exceeds array length
        }

        var mainTarget = TargetsToHit[clickNum];
        AudioSource source = mainTarget.GetComponent<AudioSource>();

        for (int i = 0; i < TargetsToHit.Length; i++)
        {
            if (TargetsToHit[i] == mainTarget)
            {
                Debug.Log("Now Sound Playing at " + mainTarget.name);
                mainTarget.SetActive(true);

                // Apply spatial settings
                source.spatialize = true;
                source.spatialBlend = 1;
                source.spatializePostEffects = true;

                // Apply volume settings based on SOFA
                switch (hrtfIndex)
                {
                    case 0:
                        source.volume = PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
                        break;
                    case 1:
                        source.volume = PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
                        break;
                    case 2:
                        source.volume = PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);
                        break;
                }

                source.loop = true;
                source.clip = Stimuli[currentClipIndex]; // Set the initial clip
                source.Play();
            }
            else
            {
                TargetsToHit[i].SetActive(false);
                TargetsToHit[i].GetComponent<AudioSource>().Stop();
            }
        }
    }

    void Counter()
    {
        trialNum++;
        int a = trialNum; // - 1;
        Trial.text = a.ToString();
        if (a == 10)
        {
            DoneUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            var mainTarget = TargetsToHit[clickNum];
            AudioSource source = mainTarget.GetComponent<AudioSource>();
            source.Stop();
        }
    }

    void SwitchSoundClip()
    {
        if (Stimuli.Length > 0)
        {
            currentClipIndex = (currentClipIndex + 1) % Stimuli.Length; // Move to the next clip in the array

            var mainTarget = TargetsToHit[clickNum];
            AudioSource source = mainTarget.GetComponent<AudioSource>();
            source.clip = Stimuli[currentClipIndex]; // Set the new clip
            source.Play(); // Play the new clip
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SteamAudio;

public class TutorialGameSoundPlayer : MonoBehaviour
{
    public GameObject[] TargetsToHit;
    public GameObject DoneUI;
    public Text Trial;
    public AudioClip[] Stimuli;


    private SteamAudioManager steamAudioManager;
    private int clickNum = 0;
    private int trialNum = 0;
    private int currentClipIndex = 0;

    void Start()
    {
        steamAudioManager = FindObjectOfType<SteamAudioManager>();
        SOFASelector();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SOFASelector();
            Counter();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSoundClip();
        }

    }

    void SOFASelector()
    {
        int hrtfIndex = Random.Range(0, 3);
        steamAudioManager.currentHRTF = hrtfIndex;
        Debug.Log("Current SOFA: " + hrtfIndex);
        
        clickNum++;

        if (clickNum >= TargetsToHit.Length)
        {
            clickNum = 0; // Reset to start if exceeds array length
        }

        var mainTarget = TargetsToHit[clickNum];
        AudioSource source = mainTarget.GetComponent<AudioSource>();

        

        for (int i = 0; i < TargetsToHit.Length; i++)
        {
            if (TargetsToHit[i] == mainTarget)
            {
                Debug.Log("Now Sound Playing at " + mainTarget.name);
                mainTarget.SetActive(true);

                // Apply spatial settings
                source.spatialize = true;
                source.spatialBlend = 1;
                source.spatializePostEffects = true;

                // Apply volume settings based on SOFA
                switch (hrtfIndex)
                {
                    case 0:
                        source.volume = PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
                        break;
                    case 1:
                        source.volume = PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
                        break;
                    case 2:
                        source.volume = PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);
                        break;
                }

                source.loop = true;
                source.Play();
            }
            else
            {
                TargetsToHit[i].SetActive(false);
                TargetsToHit[i].GetComponent<AudioSource>().Stop();
            }
            
        }
    }

    void Counter()
    {
        trialNum++;
        int a = trialNum; // - 1;
        Trial.text = a.ToString();
        if (a == 10)
        {
            DoneUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            var mainTarget = TargetsToHit[clickNum];
            AudioSource source = mainTarget.GetComponent<AudioSource>();
            source.Play();

        }
        // Implement audio clip switching logic if needed
    }
    void SwitchSoundClip()
    {
        
    }

}
*/