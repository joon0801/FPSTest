using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AzimuthDetector : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    public int maxcount = 10;
    private int clickCount = 10;
    private string filePath;

    void Update()
    {
        AngleDetector();

        // 파일 경로 설정
        filePath = Application.dataPath + "/RotationAngles.csv";

        // 파일이 없으면 헤더를 작성
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "ClickNumber,RotationAngle\n");
        }
    }


    void AngleDetector()
    {
        if (Input.GetMouseButtonDown(0) && clickCount < maxcount)
        {
            clickCount++;
            float rotationYDegrees = Player.transform.eulerAngles.y;
            float rotationYRadians = rotationYDegrees * Mathf.Deg2Rad; // 각도를 라디안으로 변환

            Debug.Log("Player's rotation angle: " + rotationYDegrees + " degrees, " + rotationYRadians + " radians");

            // 파일에 각도 기록
            string newLine = clickCount + "," + rotationYDegrees + "," + rotationYRadians + "\n";
            File.AppendAllText(filePath, newLine);
        }

    }
}
