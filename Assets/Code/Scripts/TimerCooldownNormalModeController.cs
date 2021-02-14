using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCooldownNormalModeController : MonoBehaviour
{
    [SerializeField]
    private Settings globalSettings;

    void Start()
    {
        StartCoroutine(RunTimerCooldownNormalMode());
    }
    IEnumerator RunTimerCooldownNormalMode()
    {
        float time;
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
            EnemyCooldownNormalMode.currentTime = globalSettings.cooldownSpeedNormalMode;

        } while (true);
    }
}
