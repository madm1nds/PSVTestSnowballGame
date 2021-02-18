using System.Collections;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "ItemsButton" в главном меню
/// </summary>
public class ItemsButton : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        //for (int i = 0; i < Vault.instance.buttonUIMainMenu.Length; i++)
        //{
        //    if (Vault.instance.buttonUIMainMenu[i].CompareTag("ItemsButton"))
        //    {
        //        Vault.instance.buttonUIMainMenu[i].onClick.AddListener(delegate { ShowItemsMenu(); });
        //        break;
        //    }
        //}
    }
    /// <summary>
    /// Активирует переход в выбор снежков, после завершения анимации.
    /// </summary>
    void ShowItemsMenu()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowItemsMenu;
        for (int i = 0; i < Vault.instance.gameObjectMainMenu.Length; i++)
        {
            Vault.instance.gameObjectMainMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
    }
}
