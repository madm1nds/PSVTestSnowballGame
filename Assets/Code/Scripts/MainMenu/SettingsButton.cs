using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "SettingsButton" в главном меню.
/// </summary>
public class SettingsButton : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        //for (int i = 0; i < Vault.instance.buttonUIMainMenu.Length; i++)
        //{
        //    if (Vault.instance.buttonUIMainMenu[i].CompareTag("SettingsButton"))
        //    {
        //        Vault.instance.buttonUIMainMenu[i].onClick.AddListener(delegate { ShowSettingsMenu(); });
        //        break;
        //    }
        //}
    }
    /// <summary>
    /// Активирует переход в настройки, после завершения анимации.
    /// </summary>
    void ShowSettingsMenu()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowSettingsMenu;
        for (int i = 0; i < Vault.instance.gameObjectMainMenu.Length; i++)
        {
            Vault.instance.gameObjectMainMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
    }
}
