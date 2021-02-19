using System.Collections;
using UnityEngine;
/// <summary>
/// Класс, который заполняет перезарядку Hippo. При заполнении 
/// переводит поле isThrow в значение false, что означет, что можно снова использовать
/// атакующую срособность.
/// </summary>
public class StatusBarAbilityController : MonoBehaviour
{
    public static StatusBarAbilityController instance;

    public static bool isThrow = false;
    private const float abilityStatusEndPosition = 4.35f;

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
        Vault.instance.gameObjectHippoReadySnowball.SetActive(false);
        Vault.instance.gameObjectStatusBarAbility.gameObject.SetActive(true);
        isThrow = true;
        Vault.instance.transformStatusBarAbilityMask.localPosition = new Vector3(0f, Vault.instance.transformStatusBarAbilityMask.localPosition.y, Vault.instance.transformStatusBarAbilityMask.localPosition.z);
        do
        {
            if (PauseButtonController.isPause == false)
            {
                yield return new WaitForSeconds(0.01f);
                Vault.instance.transformStatusBarAbilityMask.localPosition = new Vector3(
                    Vault.instance.transformStatusBarAbilityMask.localPosition.x + (0.072f / Vault.instance.settings.speedCooldownHippo),
                        Vault.instance.transformStatusBarAbilityMask.localPosition.y, Vault.instance.transformStatusBarAbilityMask.localPosition.z);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        while (Vault.instance.transformStatusBarAbilityMask.localPosition.x < abilityStatusEndPosition);
        isThrow = false;
        Vault.instance.gameObjectHippoReadySnowball.SetActive(true);
        Vault.instance.gameObjectStatusBarAbility.gameObject.SetActive(false);
        yield break;
    }
    /// <summary>
    /// Безопастный вызов извне.
    /// </summary>
    public void InvokeChangeStatus()
    {
        StartCoroutine(ChangeStatus());
    }
}
