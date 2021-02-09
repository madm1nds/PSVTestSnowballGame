using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerRoundController : MonoBehaviour
{
    [SerializeField]
    private TextPictureConverter textPictureConverter;
    [SerializeField]
    private GameObject timerSet;

    public static int currentTime;
    private int minutes;
    private int seconds;

    void Start()
    {
        currentTime = -1;
        StartCoroutine(RunTimer());
    }
    IEnumerator RunTimer()
    {
        do
        {            
            string currentTimeString = "";            
            
            currentTime++;
            minutes = currentTime / 60;
            seconds = currentTime - (60 * minutes);
            if (minutes < 10)
            {
                if (minutes == 0)
                {
                    currentTimeString += "00:";
                }
                else
                {
                    currentTimeString += "0" + minutes + ":";
                }
            }
            else
            {
                currentTimeString += minutes + ":";
            }


            if (seconds < 10)
            {
                if (seconds == 0)
                {
                    currentTimeString += "00";
                }
                else
                {
                    currentTimeString += "0" + seconds;
                }
            }
            else
            {
                currentTimeString += seconds;
            }
            textPictureConverter.SetImageNumber(timerSet, currentTimeString, 10, 10, AlignmentTextPicture.Right);
            yield return new WaitForSeconds(1f);
        } while (true);
    }
}
