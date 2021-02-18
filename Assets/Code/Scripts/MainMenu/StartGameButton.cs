using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "StartGameButton" в главном меню.
/// </summary>
public class StartGameButton : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        mainMenuButtons("StartGameButton", AnimationActions.NameAnimation.ShowStartGameMenu);
        mainMenuButtons("SettingsButton", AnimationActions.NameAnimation.ShowSettingsMenu);
        mainMenuButtons("ItemsButton", AnimationActions.NameAnimation.ShowItemsMenu);
        //for (int i = 0; i < Vault.instance.buttonUIMainMenu.Length; i++)
        //{
        //    if (Vault.instance.buttonUIMainMenu[i].CompareTag("StartGameButton"))
        //    {
        //        Vault.instance.buttonUIMainMenu[i].onClick.AddListener(delegate { ShowStartGameMenu(); });
        //        break;
        //    }
        //}
    }
    /// <summary>
    /// Активирует переход в выбор уровней, после завершения анимации.
    /// </summary>
    void ShowStartGameMenu()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowStartGameMenu;
        for (int i = 0; i < Vault.instance.gameObjectMainMenu.Length; i++)
        {
            Vault.instance.gameObjectMainMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
    }
    void mainMenuButtons(String CompareTag, AnimationActions.NameAnimation nameAnimation)
    {
        for (int i = 0; i < Vault.instance.buttonUIMainMenu.Length; i++)
        {
            if (Vault.instance.buttonUIMainMenu[i].CompareTag(CompareTag))
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
