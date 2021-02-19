using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "BackButton" в главном меню
/// </summary>
public class BackButton : MonoBehaviour
{
    private const float delayBeforeInitialization = 0.2f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeInitialization);
        Vault.instance.buttonUIBackButton.onClick.AddListener(delegate { ShowMainMenu(); });
    }
    /// <summary>
    /// Активирует переход в главное меню, после завершения анимации.
    /// </summary>
    void ShowMainMenu()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowMainMenu;
        RunAnimation(Vault.instance.gameObjectItemsMenu, Vault.instance.gameObjectsItemsMenu);
        RunAnimation(Vault.instance.gameObjectStartGameMenu, Vault.instance.gameObjectsStartGameMenu);
        RunAnimation(Vault.instance.gameObjectSettingsMenu, Vault.instance.gameObjectsSettingsMenu);
        Vault.instance.audioSourcePressButton.Play();
    }
    /// <summary>
    /// Запускает анимацию каждого объекта в определённой части меню.
    /// </summary>
    /// <param name="currentPage">Текущая часть меню.</param>
    /// <param name="currentPageObjects">Объекты активной части меню.</param>
    void RunAnimation(GameObject currentPage, GameObject[] currentPageObjects)
    {
        if (currentPage.activeInHierarchy == true)
        {
            for (int i = 0; i < currentPageObjects.Length; i++)
            {
                currentPageObjects[i].GetComponent<Animator>().SetTrigger("Exit");
            }
        }
    }
}
