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
        do
        {           
            yield return new WaitForSeconds(globalSettings.cooldownSpeedNormalMode);
            EnemyCooldownNormalMode.currentTime = globalSettings.cooldownSpeedNormalMode;
        } while (true);
    }
}
