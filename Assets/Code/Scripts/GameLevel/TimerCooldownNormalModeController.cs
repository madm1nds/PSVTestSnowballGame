using System.Collections;
using UnityEngine;
/// <summary>
/// Класс блокирующий атакующие способности противников, пока идёт перезарядка (Работает только в NormalMode)
/// </summary>
public class TimerCooldownNormalModeController : MonoBehaviour
{
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
            } while (time < Vault.instance.settings.cooldownSpeedNormalMode);

            time = 0;
            isTimeOut = true;

        } while (true);
    }
}
