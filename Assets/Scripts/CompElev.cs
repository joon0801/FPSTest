using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompElev : MonoBehaviour
{
    public RawImage CompassElev;
    public Transform Player;
    public Text ElevText;

    // Update is called once per frame
    public void Update()
    {
        
        CompassElev.uvRect = new Rect(Player.localEulerAngles.x  / 180, 0, 1, 1);

        // Get a copy of your forward vector
        Vector3 forward = Player.transform.forward;
        //if (forward != Vector3.zero)

        // Zero out the y component of your forward vector to only get the direction in the X,Z plane
        forward.x = 0;
        
        
        //Clamp our angles to only 5 degree increments
        float headingAngle = Quaternion.LookRotation(forward).eulerAngles.x;        
            headingAngle = 5 * (Mathf.RoundToInt(headingAngle / 5.0f));

        //Convert float to int for switch
        int displayangle;
        displayangle = Mathf.RoundToInt(headingAngle);

        //Set the text of Compass Degree Text to the clamped value, but change it to the letter if it is a True direction
        
        
        switch (displayangle)
        {
            case 0:
                ElevText.text = "Ear Level";
                break;

            case 360:                
                ElevText.text = "Ear Level";
                break;
            case 355:
                ElevText.text = "5 - OverHead";
                break;
            case 350:
                ElevText.text = "10 - OverHead";
                break;
            case 345:
                ElevText.text = "15 - OverHead";
                break;
            case 340:
                ElevText.text = "20 - OverHead";
                break;
            case 335:
                ElevText.text = "25 - OverHead";
                break;
            case 330:
                ElevText.text = "30 - OverHead";
                break;
            case 325:
                ElevText.text = "35 - OverHead";
                break;
            case 320:
                ElevText.text = "40 - OverHead";
                break;
            case 315:
                ElevText.text = "45 - OverHead";
                break;
            case 310:
                ElevText.text = "50 - OverHead";
                break;
            case 305:
                ElevText.text = "55 - OverHead";
                break;
            case 300:
                ElevText.text = "60 - OverHead";
                break;
            case 295:
                ElevText.text = "65 - OverHead";
                break;
            case 290:
                ElevText.text = "70 - OverHead";
                break;
            case 285:
                ElevText.text = "75 - OverHead";
                break;
            case 280:
                ElevText.text = "80 - OverHead";
                break;
            case 275:
                ElevText.text = "85 - OverHead";
                break;
            case 270:
                ElevText.text = "Top - OverHead";
                break;


            case 5:
                ElevText.text = "- 5 - Below";
                break;
            case 10:
                ElevText.text = "- 10 - Below";
                break;
            case 15:
                ElevText.text = "- 15 - Below";
                break;
            case 20:
                ElevText.text = "- 20 - Below";
                break;
            case 25:
                ElevText.text = "- 25 - Below";
                break;
            case 30:
                ElevText.text = "- 30 - Below";
                break;
            case 35:
                ElevText.text = "- 35 - Below";
                break;
            case 40:
                ElevText.text = "- 40 - Below";
                break;
            case 45:
                ElevText.text = "- 45 - Below";
                break;
            case 50:
                ElevText.text = "- 50 - Below";
                break;
            case 55:
                ElevText.text = "- 55 - Below";
                break;
            case 60:
                ElevText.text = "- 60 - Below";
                break;
            case 65:
                ElevText.text = "- 65 - Below";
                break;
            case 70:
                ElevText.text = "- 70 - Below";
                break;
            case 75:
                ElevText.text = "- 75 - Below";
                break;
            case 80:
                ElevText.text = "- 80 - Below";
                break;
            case 85:
                ElevText.text = "- 85 - Below";
                break;
            case 90:
                ElevText.text = "Bottom - Below";
                break;



            default:
                ElevText.text = headingAngle.ToString();
                break;
        }
    }
}
