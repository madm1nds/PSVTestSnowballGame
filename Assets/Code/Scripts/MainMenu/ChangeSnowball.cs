using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Изменяет внешний вид снежков. 
/// При выборе определённого снежка, меняется цветовая гамма кнопок в меню выбора снежков.
/// </summary>
public static class ChangeSnowball
{

    /// <summary>
    /// Запускает изменения
    /// </summary>
    /// <param name="button">Нажимаемая кнопка</param>
    /// <param name="currentNumberSnowball">Номер снежка-спрайта</param>
    public static void Run(GameObject button, int currentNumberSnowball)
    {
        Vector4 colorButton = new Vector4(0, 199f / 255f, 1, 1);
        Vault.instance.gameObjectHippoReadySnowball.GetComponent<SpriteRenderer>().sprite = Vault.instance.spriteSnowball[currentNumberSnowball];
        for (int i = 0; i < Vault.instance.gameObjectHippoSnowballSet.Length; i++)
        {
            Vault.instance.gameObjectHippoSnowballSet[i].GetComponent<SpriteRenderer>().sprite = Vault.instance.spriteSnowball[currentNumberSnowball];
        }
        Vault.instance.imageUISnowballButton.sprite = Vault.instance.spriteSnowball[currentNumberSnowball];
        for (int i = 0; i < Vault.instance.buttonUIItemsMenu.Length; i++)
        {
            if (Vault.instance.buttonUIItemsMenu[i].CompareTag("Snowball"))
            {
                Vault.instance.buttonUIItemsMenu[i].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
        }
        button.GetComponent<Image>().color = colorButton;
        if (Vault.startPosition < 2)
        {
            Vault.startPosition++;
        }
        else
        {
            Vault.instance.audioSourcePressButton.Play();
        }
    }
}
