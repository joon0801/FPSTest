using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;

public class CsvLogger : MonoBehaviour
{
    private string filePath;
    private List<string[]> rowData = new List<string[]>();
    private int currentNum = 0;

    void Start()
    {
        // CSV 파일 경로 설정
        string currentTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        filePath = GetDesktopPath($"log_{currentTime}.csv");

        // CSV 파일에 헤더 추가
        string[] rowDataTemp = new string[10];
        rowDataTemp[0] = "No.";
        rowDataTemp[1] = "Target Azimuth";
        rowDataTemp[2] = "Target Elevation";
        rowDataTemp[3] = "Stimuli";
        rowDataTemp[4] = "HRTF type";
        rowDataTemp[5] = "Repetition";
        rowDataTemp[6] = "Player’s Azimuth";
        rowDataTemp[7] = "Player’s Elevation";
        rowDataTemp[8] = "Reaction Time";
        rowDataTemp[9] = "FlagMinus";
        rowData.Add(rowDataTemp);

        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            var line = string.Join(",", rowDataTemp);
            sw.WriteLine(line);
        }
    }

    public void LogData(float azimuth, float elevation, string stimuli, string hrtf, int repetition, float playerAzimuth, float playerElevation, float timeToClick, int flag)
    {
        /*
        // Player's Azimuth 변환 (0~360도 -> -180~180도)
        if (playerAzimuth > 180)
        {
            playerAzimuth -= 360;
        }

        // Player's Elevation 변환 (정면을 기준으로 위 0~90도, 아래 0~-90도)
        if (playerElevation > 180)
        {
            playerElevation -= 360;
        }
        */
        playerElevation *= -1;
        Debug.Log($"InCSV: Azim : {playerAzimuth}, Elev : {playerElevation}");

        // 각도를 라디안으로 변환
        // float playerAzimuthRadians = playerAzimuth * Mathf.Deg2Rad;
        // float playerElevationRadians = playerElevation * Mathf.Deg2Rad;

        string[] rowDataTemp = new string[10];
        // rowDataTemp[0] = azimuth.ToString();
        // rowDataTemp[1] = elevation.ToString();
        // rowDataTemp[2] = stimuli;
        // rowDataTemp[3] = hrtf;
        // rowDataTemp[4] = repetition.ToString();
        // rowDataTemp[5] = playerAzimuth.ToString();
        // rowDataTemp[6] = playerAzimuthRadians.ToString();
        // rowDataTemp[7] = playerElevation.ToString();
        // rowDataTemp[8] = playerElevationRadians.ToString();
        // rowDataTemp[9] = timeToClick.ToString();

        currentNum++;
        rowDataTemp[0] = currentNum.ToString();
        rowDataTemp[1] = azimuth.ToString();
        rowDataTemp[2] = elevation.ToString();
        rowDataTemp[3] = stimuli;
        rowDataTemp[4] = hrtf;
        rowDataTemp[5] = repetition.ToString();
        rowDataTemp[6] = playerAzimuth.ToString();
        rowDataTemp[7] = playerElevation.ToString();
        rowDataTemp[8] = timeToClick.ToString();
        rowDataTemp[9] = flag.ToString();

        // rowData.Add(rowDataTemp);

        // // 데이터를 정렬합니다.
        // var sortedData = rowData.Skip(1).OrderBy(row => float.Parse(row[0])) // Azimuth
        //                                   .ThenBy(row => float.Parse(row[1])) // Elevation
        //                                   .ThenBy(row => row[2])              // Stimuli
        //                                   .ThenBy(row => row[3])              // HRTF
        //                                   .ThenBy(row => int.Parse(row[4]))   // Repetition
        //                                   .ThenBy(row => float.Parse(row[5])) // Player's Azimuth
        //                                   .ThenBy(row => float.Parse(row[7])) // Player's Elevation
        //                                   .ThenBy(row => float.Parse(row[9])) // Time to click
        //                                   .ToList();

        // sortedData.Insert(0, rowData[0]); // 헤더를 다시 추가합니다.

        // CSV 파일에 데이터를 씁니다.
        // StringBuilder sb = new StringBuilder();
        // // foreach (var row in sortedData)
        // foreach (var row in rowDataTemp)
        // {
        //     sb.AppendLine(string.Join(",", row));
        // }
        // Debug.Log(sb.ToString());
        // File.WriteAllText(filePath, sb.ToString());
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            var line = string.Join(",", rowDataTemp);
            // Debug.Log(line);
            sw.WriteLine(line);
        }
    }

    private string GetDesktopPath(string fileName)
    {
        string desktopPath = string.Empty;
#if UNITY_STANDALONE_WIN
        desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
#elif UNITY_STANDALONE_OSX
        desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
#endif
        return Path.Combine(desktopPath, fileName);
    }
}









/*
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public class CsvLogger : MonoBehaviour
{
    private string filePath;
    private List<string[]> rowData = new List<string[]>();

    void Start()
    {
        // CSV 파일 경로 설정
        filePath = GetDesktopPath("log.csv");

        // CSV 파일에 헤더 추가
        string[] rowDataTemp = new string[11];
        rowDataTemp[0] = "Trial No.";
        rowDataTemp[1] = "Azimuth";
        rowDataTemp[2] = "Elevation";
        rowDataTemp[3] = "Stimuli";
        rowDataTemp[4] = "HRTF";
        rowDataTemp[5] = "Repetition";
        rowDataTemp[6] = "Player’s Azimuth";
        rowDataTemp[7] = "Player’s Azimuth (Radians)";
        rowDataTemp[8] = "Player’s Elevation";
        rowDataTemp[9] = "Player’s Elevation (Radians)";
        rowDataTemp[10] = "Time to click";
        rowData.Add(rowDataTemp);
    }

    public void LogData(int Trial, float azimuth, float elevation, string stimuli, string hrtf, int repetition, float playerAzimuth, float playerElevation, float timeToClick)
    {
        // Player's Azimuth 변환 (0~360도 -> -180~180도)
        //if (playerAzimuth > 180)
        //{
        //    playerAzimuth -= 360;
        //}

        //// Player's Elevation 변환 (정면을 기준으로 위 0~90도, 아래 0~-90도)
        //if (playerElevation > 180)
        //{
        //    playerElevation -= 360;
        //}

        // 각도를 라디안으로 변환
        float playerAzimuthRadians = playerAzimuth * Mathf.Deg2Rad;
        float playerElevationRadians = playerElevation * Mathf.Deg2Rad;

        string[] rowDataTemp = new string[11];
        rowDataTemp[0] = Trial.ToString();
        rowDataTemp[1] = azimuth.ToString();
        rowDataTemp[2] = elevation.ToString();
        rowDataTemp[3] = stimuli;
        rowDataTemp[4] = hrtf;
        rowDataTemp[5] = repetition.ToString();
        rowDataTemp[6] = playerAzimuth.ToString();
        rowDataTemp[7] = playerAzimuthRadians.ToString();
        rowDataTemp[8] = playerElevation.ToString();
        rowDataTemp[9] = playerElevationRadians.ToString();
        rowDataTemp[10] = timeToClick.ToString();
        rowData.Add(rowDataTemp);

        // 데이터를 정렬합니다.
        var sortedData = rowData.Skip(1).OrderBy(row => int.Parse(row[0]))
                                          .ThenBy(row => float.Parse(row[1])) // Azimuth
                                          .ThenBy(row => float.Parse(row[2])) // Elevation
                                          .ThenBy(row => row[3])              // Stimuli
                                          .ThenBy(row => row[4])              // HRTF
                                          .ThenBy(row => int.Parse(row[5]))   // Repetition
                                          .ThenBy(row => float.Parse(row[6])) // Player's Azimuth
                                          .ThenBy(row => float.Parse(row[8])) // Player's Elevation
                                          .ThenBy(row => float.Parse(row[10])) // Time to click
                                          .ToList();

        sortedData.Insert(0, rowData[0]); // 헤더를 다시 추가합니다.

        // CSV 파일에 데이터를 씁니다.

        StringBuilder sb = new StringBuilder();
        foreach (var row in sortedData)
        {
            sb.AppendLine(string.Join(",", row));
        }
        File.WriteAllText(filePath, sb.ToString());

        /*
        StringBuilder sb = new StringBuilder();
        // foreach (var row in sortedData)
        foreach (var row in rowDataTemp)
        {
            sb.AppendLine(string.Join(",", row));
        }
        // File.WriteAllText(filePath, sb.ToString());
        File.AppendAllText(filePath, sb.ToString());
        //////////
    }

    private string GetDesktopPath(string fileName)
    {
        string desktopPath = string.Empty;
#if UNITY_STANDALONE_WIN
        desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
#elif UNITY_STANDALONE_OSX
        desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
#endif
        return Path.Combine(desktopPath, fileName);
    }
}

*/