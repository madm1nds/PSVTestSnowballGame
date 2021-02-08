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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeStatus());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    IEnumerator ChangeStatus()
    {
        do
        {
            yield return new WaitForSeconds(0.001f);
            if (statusBarAbility.gameObject.activeInHierarchy == true)
            {
                if (statusBarAbilityMask.localPosition.x < 4.35f)
                {
                    statusBarAbilityMask.localPosition = new Vector3(statusBarAbilityMask.localPosition.x + (0.072f / selectedTime.cooldown), statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
                }
                else
                {
                    statusBarAbilityMask.localPosition = new Vector3(0f, statusBarAbilityMask.localPosition.y, statusBarAbilityMask.localPosition.z);
                }
            }
        } while (true);
    }
}
