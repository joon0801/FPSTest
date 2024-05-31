using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SteamAudio;

public class CalibrationManager : MonoBehaviour
{
    public Slider sofaSlider_Generic;
    public Slider sofaSlider_3D_Based;
    public Slider sofaSlider_DL_Based;
    public Text volumeText_Generic;
    public Text volumeText_3D_Based;
    public Text volumeText_DL_Based;
    public Button playButton_Generic;
    public Button playButton_3D_Based;
    public Button playButton_DL_Based;

    public AudioSource audioSource_Generic; // Generic용 AudioSource
    public AudioSource audioSource_3D_Based; // 3D_Based용 AudioSource
    public AudioSource audioSource_DL_Based; // DL_Based용 AudioSource

    public AudioClip genericClip; // Unity 에디터에서 드래그 앤 드롭
    public AudioClip threeDBasedClip; // Unity 에디터에서 드래그 앤 드롭
    public AudioClip dlBasedClip; // Unity 에디터에서 드래그 앤 드롭

    private SteamAudioManager steamAudioManager;

    void Start()
    {
        steamAudioManager = FindObjectOfType<SteamAudioManager>();

        // 슬라이더 값이 변경될 때 이벤트 설정
        sofaSlider_Generic.onValueChanged.AddListener(delegate { OnSliderValueChanged(sofaSlider_Generic, "Volume_Generic", volumeText_Generic); });
        sofaSlider_3D_Based.onValueChanged.AddListener(delegate { OnSliderValueChanged(sofaSlider_3D_Based, "Volume_3D_Based", volumeText_3D_Based); });
        sofaSlider_DL_Based.onValueChanged.AddListener(delegate { OnSliderValueChanged(sofaSlider_DL_Based, "Volume_DL_Based", volumeText_DL_Based); });

        // 초기 슬라이더 값 설정
        sofaSlider_Generic.value = PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
        sofaSlider_3D_Based.value = PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
        sofaSlider_DL_Based.value = PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);

        UpdateVolumeText(sofaSlider_Generic, volumeText_Generic);
        UpdateVolumeText(sofaSlider_3D_Based, volumeText_3D_Based);
        UpdateVolumeText(sofaSlider_DL_Based, volumeText_DL_Based);

        // 플레이 버튼 클릭 시 이벤트 설정
        playButton_Generic.onClick.AddListener(delegate { PlaySound("Generic"); });
        playButton_3D_Based.onClick.AddListener(delegate { PlaySound("3D_Based"); });
        playButton_DL_Based.onClick.AddListener(delegate { PlaySound("DL_Based"); });

        // AudioClip 설정
        audioSource_Generic.clip = genericClip;
        audioSource_3D_Based.clip = threeDBasedClip;
        audioSource_DL_Based.clip = dlBasedClip;

        // AudioClip이 설정되었는지 확인
        if (audioSource_Generic.clip == null) Debug.LogError("GenericClip not assigned");
        if (audioSource_3D_Based.clip == null) Debug.LogError("3D_BasedClip not assigned");
        if (audioSource_DL_Based.clip == null) Debug.LogError("DL_BasedClip not assigned");
    }

    void OnSliderValueChanged(Slider slider, string playerPrefsKey, Text volumeText)
    {
        PlayerPrefs.SetFloat(playerPrefsKey, slider.value);
        UpdateVolumeText(slider, volumeText);
        ApplyVolumeToPlayingAudio();
    }

    void UpdateVolumeText(Slider slider, Text volumeText)
    {
        volumeText.text = $"Volume: {slider.value * 100:F0}%";
    }

    void ApplyVolumeToPlayingAudio()
    {
        audioSource_Generic.volume = PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
        audioSource_3D_Based.volume = PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
        audioSource_DL_Based.volume = PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);
    }

    void PlaySound(string sofaMethod)
    {
        // 모든 오디오 소스 중지
        audioSource_Generic.Stop();
        audioSource_3D_Based.Stop();
        audioSource_DL_Based.Stop();

        switch (sofaMethod)
        {
            case "Generic":
                steamAudioManager.currentHRTF = 0; // Generic HRTF 설정
                audioSource_Generic.volume = PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
                if (audioSource_Generic.clip != null)
                {
                    audioSource_Generic.Play();
                    Debug.Log("Playing GenericClip");
                }
                else
                {
                    Debug.LogError("GenericClip is null");
                }
                break;
            case "3D_Based":
                steamAudioManager.currentHRTF = 1; // 3D_Based HRTF 설정
                audioSource_3D_Based.volume = PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
                if (audioSource_3D_Based.clip != null)
                {
                    audioSource_3D_Based.Play();
                    Debug.Log("Playing 3D_BasedClip");
                }
                else
                {
                    Debug.LogError("3D_BasedClip is null");
                }
                break;
            case "DL_Based":
                steamAudioManager.currentHRTF = 2; // DL_Based HRTF 설정
                audioSource_DL_Based.volume = PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);
                if (audioSource_DL_Based.clip != null)
                {
                    audioSource_DL_Based.Play();
                    Debug.Log("Playing DL_BasedClip");
                }
                else
                {
                    Debug.LogError("DL_BasedClip is null");
                }
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(4); // 게임 씬의 이름을 적절히 변경하세요.
    }
}
