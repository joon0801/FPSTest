using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AzimuthDetector : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    private int clickCount = 0;
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

        if (Input.GetMouseButtonDown(0) && clickCount < 20)
        {

            clickCount++;
            float rotationY = Player.transform.eulerAngles.y;
            //rotationY = rotationY - 180.0f;
           
            Debug.Log("Player's rotation angle: " + rotationY + " degrees");

            // 파일에 각도 기록
            string newLine = clickCount + "," + rotationY + "\n";
            File.AppendAllText(filePath, newLine);

        }

    }
}
