using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationIndicator : MonoBehaviour
{
    public float Azimuth;
    public float Elevation;
    public AudioClip[] audioClips;

    void Start()
    {
        // Azimuth와 Elevation 값을 이용하여 초기 설정 작업 수행
        //Debug.Log($"Azimuth: {Azimuth}, Elevation: {Elevation}");

        // AudioSource 컴포넌트 설정
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioClips.Length > 0)
        {
            audioSource.clip = audioClips[0]; // 초기 클립 설정
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.spatialize = true; // Spatialize 설정 활성화
            audioSource.spatialBlend = 1.0f; // 3D 사운드로 설정
        }
    }

}
