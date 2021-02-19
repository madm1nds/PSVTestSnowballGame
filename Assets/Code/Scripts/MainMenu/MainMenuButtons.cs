using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопок в главном меню.
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    private const float delayBeforeInitialization = 0.2f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeInitialization);
        AddListners("StartGameButton", AnimationActions.NameAnimation.ShowStartGameMenu);
        AddListners("SettingsButton", AnimationActions.NameAnimation.ShowSettingsMenu);
        AddListners("ItemsButton", AnimationActions.NameAnimation.ShowItemsMenu);
    }
    /// <summary>
    /// Активирует переход в выборанное меню, после завершения анимации.
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
                        Vault.instance.audioSourcePressButton.Play();
                    });
                break;
            }
        }

    }
}
