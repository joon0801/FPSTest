using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SteamAudio;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;
using Unity.VisualScripting;

public class JSON_tester : MonoBehaviour
{
    [System.Serializable]
    public class DataItem
    {
        public float Azimuth;
        public float Elev;
        public int Repetition;
        public string Stimuli; // "Footstep" 또는 "PinkNoise"가 들어옴
        public string SOFA_Method;
    }

    [System.Serializable]
    public class DataList
    {
        public DataItem[] data;
    }

    public GameObject ScoreUI;
    public TextAsset jsonFile;  // TextAsset을 사용하여 JSON 파일을 참조
    public GameObject[] gameObjects; // 게임 오브젝트 배열
    public CsvLogger csvLogger; // CsvLogger 스크립트
    public GameObject player;   // 플레이어 오브젝트
    public GameObject cameraObject; // 카메라 오브젝트
    public Text Trial;
    public GameObject ErrorMessage;
    public GameObject panel;


    private DataList dataList;
    private List<DataItem> dataItems;
    private int currentIndex = 0;
    private bool isPlaying = false;
    private bool allActionsCompleted = false; // 모든 경우의 수가 사용되었는지 확인
    private SteamAudioManager steamAudioManager; // Steam Audio 매니저
    private int trialNum = 0;
    private int flag;

    private float playbackStartTime; // 음악 재생 시작 시간
    private float reactionTime;
    private bool isMusicPlaying = false; // 음악 재생 중인지 확인
    private bool isPanelActive = false;

    public int DataItemCount => dataItems?.Count ?? 0; // public 속성 추가
    public int CurrentIndex => currentIndex; // 현재 인덱스를 반환하는 public 속성 추가

    void Start()
    {
        steamAudioManager = FindObjectOfType<SteamAudioManager>();

        if (jsonFile != null)
        {
            dataList = JsonUtility.FromJson<DataList>(jsonFile.text);
            dataItems = new List<DataItem>(dataList.data);
            Shuffle(dataItems);

            // 게임 시작 시 첫 번째 소리 재생
            if (dataItems.Count > 0)
            {
                currentIndex = 0; // Ensure currentIndex is set to 0 at the start
                StartCoroutine(PerformAction(dataItems[currentIndex]));
            }
        }
        else
        {
            Debug.LogError("JSON 파일을 지정하지 않았습니다.");
        }
    }

    void Update()
    {
        ScoreUI.SetActive(false);

        if (Input.GetMouseButtonDown(0) && !isPlaying && !allActionsCompleted && !isPanelActive) // 마우스 클릭 감지
        {
            Counter();
            if (currentIndex < dataItems.Count)
            {
                Debug.Log(currentIndex);
                // 플레이어가 마우스를 클릭한 시간을 기록하고 음악 재생 시간과의 차이를 계산
                if (isMusicPlaying)
                {
                    float clickTime = Time.time;
                    reactionTime = clickTime - playbackStartTime;
                    Debug.Log($"Player clicked after {reactionTime} seconds since music started.");

                    // 현재 플레이어 시야 각도 가져오기
                    float playerAzimuth = player.transform.eulerAngles.y;
                    float playerElevation = cameraObject.transform.eulerAngles.x;
                    Debug.Log($"Azim : {playerAzimuth}, Elev : {playerElevation}");
                    // 현재 JSON 데이터 항목 가져오기
                    DataItem currentItem = dataItems[currentIndex]; // - 1];

                    // CSV 로그 기록
                    csvLogger.LogData(currentItem.Azimuth, currentItem.Elev, currentItem.Stimuli, currentItem.SOFA_Method, currentItem.Repetition, playerAzimuth, playerElevation, reactionTime, flag);

                    isMusicPlaying = false;
                }

                currentIndex++;
            }
            if (currentIndex < dataItems.Count)
            {
                StartCoroutine(PerformAction(dataItems[currentIndex]));
                if ((currentIndex == 84 || currentIndex == 168 || currentIndex == 252)) // && !isPanelVisible)
                {
                    panel.SetActive(true); // Show the panel if CurrentIndex is 10, 200, or 300 and the panel is not already visible
                    isPanelActive = true; // 패널이 활성화 상태로 설정
                    // StopAllSounds(); make in weekend
                }
            }
            else
            {
                allActionsCompleted = true;
                StopAllSounds();
                Debug.Log("모든 작업을 완료했습니다.");
                // 모든 기능 비활성화
                this.enabled = false;
                ScoreUI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPanelActive) // 패널이 활성화된 상태에서 스페이스바가 눌렸는지 확인
        {
            panel.SetActive(false);
            isPanelActive = false;
            playbackStartTime = Time.time;
            reactionTime = Time.time;
            

        }
    }

    public void clickMainMenu()
    {
        Debug.Log("Scene Changes To Main Menu");
        SceneManager.LoadScene(0);
    }

    IEnumerator PerformAction(DataItem item)
    {
        isPlaying = true;

        // 모든 오브젝트의 오디오 소스를 중지
        StopAllSounds();

        int targetAzimuth = Mathf.RoundToInt(item.Azimuth);
        int targetElevation = Mathf.RoundToInt(item.Elev);

        if (targetAzimuth == 30 || targetAzimuth == 60 || targetAzimuth == 90 || targetAzimuth == 120 || targetAzimuth == 150)
        {
            // 랜덤하게 음수 또는 양수로 변환
            if (Random.value > 0.5f)
            {
                flag = 1;
                targetAzimuth = -targetAzimuth;
            }
            else
            {
                flag = 0;
            }
        }


        GameObject selectedObject = null;
        foreach (var obj in gameObjects)
        {
            var locationIndicator = obj.GetComponent<LocationIndicator>();
            if (locationIndicator != null)
            {
                int objAzimuth = Mathf.RoundToInt(locationIndicator.Azimuth);
                int objElevation = Mathf.RoundToInt(locationIndicator.Elevation);
                if (objAzimuth == targetAzimuth && objElevation == targetElevation)
                {
                    selectedObject = obj;
                    break;
                }
            }
        }

        if (selectedObject != null)
        {
            var locationIndicator = selectedObject.GetComponent<LocationIndicator>();
            Debug.Log($"Selected Object: {selectedObject.name} with Azimuth: {locationIndicator.Azimuth} and Elevation: {locationIndicator.Elevation}");
            ErrorMessage.SetActive(false);
            // SOFA_Method에 따라 Steam Audio의 HRTF 설정 변경
            int hrtfIndex = GetHRTFIndex(item.SOFA_Method);
            steamAudioManager.currentHRTF = hrtfIndex;

            // 볼륨 설정
            float volume = GetVolumeForSofaMethod(item.SOFA_Method);
            var audioSource = selectedObject.GetComponent<AudioSource>();
            audioSource.volume = volume;

            // Stimuli에 따라 오디오 재생
            int clipIndex = GetClipIndex(item.Stimuli);
            if (clipIndex >= 0 && clipIndex < locationIndicator.audioClips.Length)
            {
                AudioClip clip = locationIndicator.audioClips[clipIndex];
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.loop = true;
                    audioSource.spatialize = true; // Spatialize 설정 활성화
                    audioSource.spatialBlend = 1.0f; // 3D 사운드로 설정
                    audioSource.Play();

                    // 음악 재생 시작 시간 기록
                    playbackStartTime = Time.time;
                    isMusicPlaying = true;
                }
                else
                {
                    Debug.LogError($"AudioClip at index {clipIndex} is null on {selectedObject.name}");
                }
            }
            else
            {
                Debug.LogError($"Invalid clip index {clipIndex} for {item.Stimuli} on {selectedObject.name}");
            }
        }
        else
        {
            Debug.LogWarning("No valid objects found for the given criteria.");
        }

        isPlaying = false;
        yield return null;
    }

    void StopAllSounds()
    {
        foreach (var obj in gameObjects)
        {
            var audioSource = obj.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }

    int GetClipIndex(string stimuli)
    {
        // Stimuli 값을 인덱스로 변환하는 로직
        switch (stimuli)
        {
            case "Footstep":
                return 0; // Footstep은 배열의 첫 번째 클립
            case "PinkNoise":
                return 1; // PinkNoise는 배열의 두 번째 클립
            default:
                return -1; // 유효하지 않은 경우
        }
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    int GetHRTFIndex(string sofaMethod)
    {
        // SOFA_Method에 따른 HRTF 인덱스를 반환하는 로직
        switch (sofaMethod)
        {
            case "Generic":
                return 0;
            case "3D_Based":
                return 1;
            case "DL_Based":
                return 2;
            default:
                return 0;
        }
    }

    float GetVolumeForSofaMethod(string sofaMethod)
    {
        switch (sofaMethod)
        {
            case "Generic":
                return PlayerPrefs.GetFloat("Volume_Generic", 1.0f);
            case "3D_Based":
                return PlayerPrefs.GetFloat("Volume_3D_Based", 1.0f);
            case "DL_Based":
                return PlayerPrefs.GetFloat("Volume_DL_Based", 1.0f);
            default:
                return 1.0f;
        }
    }

    void Counter()
    {
        trialNum++;
        int a = trialNum; // - 1;
        Trial.text = a.ToString();
        if (a == dataItems.Count)
        {
            ScoreUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
    }
}