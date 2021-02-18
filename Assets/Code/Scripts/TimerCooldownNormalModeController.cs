using System.Collections;
using UnityEngine;

public class TimerCooldownNormalModeController : MonoBehaviour
{
    [SerializeField]
    private Settings globalSettings;
    public static float time;
    public static bool isTimeOut;

    void Start()
    {
        StartCoroutine(RunTimerCooldownNormalMode());
    }
    IEnumerator RunTimerCooldownNormalMode()
    {       
        time = 0;
        do
        {

            do
            {
                if (PauseButtonController.isPause == false)
                {
                    time += 0.1f;
                }
                yield return new WaitForSeconds(0.093f);
            } while (time < globalSettings.cooldownSpeedNormalMode);

            time = 0;
            isTimeOut = true;

        } while (true);
    }
}
