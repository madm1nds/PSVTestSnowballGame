using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarAbilityController : MonoBehaviour
{
    [SerializeField]
    private Transform statusBarAbilityMask;
    [SerializeField]
    private Transform statusBarAbility;
    [SerializeField]
    private Snowball selectedTime;

    [SerializeField]
    private GameObject readySnowball;

    private GameObject statusBarAbilityGameObject;
    public static bool isThrow = false;

    // Start is called before the first frame update
    void Start()
    {
        statusBarAbilityGameObject = statusBarAbility.gameObject;
    }

    IEnumerator ChangeStatus()
    {
        readySnowball.SetActive(false);
        statusBarAbility.gameObject.SetActive(true);
        isThrow = true;
        statusBarAbilityMask.localPosition = new Vector3(0f, statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
        do
        {
            yield return new WaitForSeconds(0.01f);
            statusBarAbilityMask.localPosition = new Vector3(statusBarAbilityMask.localPosition.x + ( (0.072f*selectedTime.cooldown / 10) / selectedTime.cooldown), statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
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
