﻿using System.Collections;
using System.Text;
using UnityEngine;
/// <summary>
/// Класс, считает время. Считает минуты и секунды раунда. После чего преобразует текст в спрайты.
/// </summary>
public class TimerRoundController : MonoBehaviour
{
    [SerializeField]
    private TextPictureConverter textPictureConverter;
    [SerializeField]
    private GameObject timerSet;

    public static int currentTime;
    private int minutes;
    private int seconds;

    StringBuilder currentTimeString;
    private const float indent = 10f;
    private const float spaceBetweenChars = 10f;

    void Start()
    {
        currentTime = -1;
        currentTimeString = new StringBuilder();
        StartCoroutine(RunTimer());
    }
    IEnumerator RunTimer()
    {
        do
        {
            if (PauseButtonController.isPause == false)
            {
                currentTimeString.Length = 0;

                currentTime++;
                minutes = currentTime / 60;
                seconds = currentTime - (60 * minutes);
                if (minutes < 10)
                {
                    if (minutes == 0)
                    {
                        currentTimeString.Append("00:");
                    }
                    else
                    {
                        currentTimeString.Append("0" + minutes + ":");
                    }
                }
                else
                {
                    currentTimeString.Append(minutes + ":");
                }


                if (seconds < 10)
                {
                    if (seconds == 0)
                    {
                        currentTimeString.Append("00");
                    }
                    else
                    {
                        currentTimeString.Append("0" + seconds);
                    }
                }
                else
                {
                    currentTimeString.Append(seconds);
                }
                textPictureConverter.SetImageNumber(timerSet, currentTimeString.ToString(), indent, spaceBetweenChars, AlignmentTextPicture.Right);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        } while (true);
    }
}
