using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "StartGameButton" в главном меню.
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        AddListners("StartGameButton", AnimationActions.NameAnimation.ShowStartGameMenu);
        AddListners("SettingsButton", AnimationActions.NameAnimation.ShowSettingsMenu);
        AddListners("ItemsButton", AnimationActions.NameAnimation.ShowItemsMenu);
    }
    /// <summary>
    /// Активирует переход в выбор уровней, после завершения анимации.
    /// </summary>
    void AddListners(String compareTag, AnimationActions.NameAnimation nameAnimation)
    {
        for (int i = 0; i < Vault.instance.buttonUIMainMenu.Length; i++)
        {
            if (Vault.instance.buttonUIMainMenu[i].CompareTag(compareTag))
            {
                Vault.instance.buttonUIMainMenu[i].onClick.AddListener
                    (delegate
                    {
                        AnimationActions.currentNameAnimation = nameAnimation;
                        for (int j = 0; j < Vault.instance.gameObjectMainMenu.Length; j++)
                        {
                            Vault.instance.gameObjectMainMenu[j].GetComponent<Animator>().SetTrigger("Exit");
                        }
                    });
                break;
            }
        }
    }
}
