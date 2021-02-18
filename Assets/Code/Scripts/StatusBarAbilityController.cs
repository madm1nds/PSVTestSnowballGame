using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarAbilityController : MonoBehaviour
{
    public static StatusBarAbilityController instance;
    [SerializeField]
    private Transform statusBarAbilityMask;
    [SerializeField]
    private Transform statusBarAbility;
    [SerializeField]
    private Snowball selectedTime;

    [SerializeField]
    private GameObject readySnowball;

    public static bool isThrow = false;

    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<StatusBarAbilityController>();
        }

        InvokeChangeStatus();
    }

    IEnumerator ChangeStatus()
    {
        readySnowball.SetActive(false);
        statusBarAbility.gameObject.SetActive(true);
        isThrow = true;
        statusBarAbilityMask.localPosition = new Vector3(0f, statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
        do
        {
            if (PauseButtonController.isPause == false)
            {
                yield return new WaitForSeconds(0.01f);
                statusBarAbilityMask.localPosition = new Vector3(statusBarAbilityMask.localPosition.x + (0.072f / selectedTime.cooldown), statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        } 
        while (statusBarAbilityMask.localPosition.x < 4.35f);
        isThrow = false;
        readySnowball.SetActive(true);
        statusBarAbility.gameObject.SetActive(false);
        yield break;
    }
    public void InvokeChangeStatus()
    {
        StartCoroutine(ChangeStatus());
    }
}
