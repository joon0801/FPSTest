using JetBrains.Annotations;
using SteamAudio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.PackageManager;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TargetHit : MonoBehaviour
{
    public GameObject targetArray;

    public GameObject[] TargetsToHit;
    GameObject Targets;

    Target tarcolor;
  

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip SoundHit;

    UnityEngine.Material mat;

    void Start()
    {
        GetComponent<AudioSource>();
        SteamAudioSource steamAudioSource = GetComponent<SteamAudioSource>();
        steamAudioSource.enabled= false;
        SetTarget();
        
    }
    
    void Update()
    {

        for (int i = 0; i < TargetsToHit.Length; i++)
        {
            var allSteam = TargetsToHit[i].gameObject;
            AudioSource allAud = allSteam.GetComponent<AudioSource>();
            SteamAudioSource allHrtf = allAud.GetComponent<SteamAudioSource>();
            //var targetArrayAudio = targetArray.GetComponent<AudioSource>();

            if (Input.GetKeyDown(KeyCode.G))
            {

                allAud.spatialize = false;
                allAud.spatialBlend = 0.4f;
                allAud.spatializePostEffects = false;
                allAud.volume = 1;

                allHrtf.directivity = false;
                allHrtf.dipoleWeight = 0.5f;
                allHrtf.dipolePower = 4f;
                allHrtf.directBinaural = false;
                Debug.Log("Spatializer OFF");
            }

            if (Input.GetKeyDown(KeyCode.H))
            {

                allAud.spatialize = false;
                allAud.spatialBlend = 0.8f;
                allAud.spatializePostEffects = false;
                allAud.volume = 1;

                allHrtf.directivity = false;
                allHrtf.dipoleWeight = 0.5f;
                allHrtf.dipolePower = 4f;
                allHrtf.directBinaural = false;
                Debug.Log("Default SOFA");
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                allAud.spatialize = true;
                allAud.spatialBlend = 1;
                allAud.spatializePostEffects = true;
                allAud.volume = 0.707f;

                allHrtf.directivity = true;
                allHrtf.dipoleWeight = 0.5f;
                allHrtf.dipolePower = 4f;
                allHrtf.directBinaural = true;
                Debug.Log("Spatialize ON");                
            }

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetTarget();
        }
           

        

    }      
    public void SetTarget()
    {
      

        var randomnumber = UnityEngine.Random.Range(0, TargetsToHit.Length);        
        var mainTarget = TargetsToHit[randomnumber].gameObject;
      

        AudioSource source = mainTarget.GetComponent<AudioSource>();
        SteamAudioSource hrtfsetting = GetComponent<SteamAudioSource>();
        //source.volume = 0.1f;


        for (int i = 0; i < TargetsToHit.Length; i++)
        {
            

            if (TargetsToHit[i] == mainTarget)
            {
                Debug.Log("Now Sound Playing at " + mainTarget.name);
                
                mainTarget.SetActive(true);
                mainTarget.tag = "Real Target";
                mainTarget.GetComponent<AudioSource>();
                /*
                source.spatialize = true;
                source.spatialBlend = 1;
                source.spatializePostEffects = true;
                
                
                */
                source.clip = SoundHit;
                source.loop = true;
                source.Play();

                /*
                hrtfsetting.directivity = true;
                hrtfsetting.dipoleWeight = 0.5f;
                hrtfsetting.dipolePower = 4f;
                hrtfsetting.directBinaural = true;
                */
            }
            else
            {
                TargetsToHit[i].SetActive(true);
                TargetsToHit[i].tag = "Target";
                TargetsToHit[i].GetComponent<AudioSource>().Stop();
                
            }

        }
        

        //IEnumerator HitCoroutine()
        //{



        //  yield return new WaitForSecondsRealtime(1f);
        //}








        //if(audioSource.isPlaying= false)


        // if (TargetsToHit != [randomnumber])
        {
        //    isRealTarget= false;

        }
        
        


    }
    //public void EnableHRTF()
    //{
        
    //    SteamAudioSource hrtfsetting = GetComponent<SteamAudioSource>();
        
        
    //    hrtfsetting.directivity = true;
    //    hrtfsetting.dipoleWeight = 0.5f;
    //    hrtfsetting.dipolePower = 4f;
    //    hrtfsetting.directBinaural = true;
    //}
    //public void DisableHRTF()
    //{
    //    enabled = false;
    //}


    //private bool isRealTarget = false;




}
