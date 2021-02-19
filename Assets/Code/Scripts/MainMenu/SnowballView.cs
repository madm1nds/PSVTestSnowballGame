using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Добавляет логику для кнопок "Snowball_0", "Level_...", "Level_N" в выборе снежков (ItemMenu->ItemVault).
/// Содержит открытое статическое поле "currentNumberSnowball", которое указывает, какой вид снежка был выбран.
/// instance - открытая ссылка на самого себя.
/// </summary>
public class SnowballView : MonoBehaviour
{
    public static SnowballView instance;
    public int currentNumberSnowball;

    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<SnowballView>();
        }

        gameObject.GetComponent<Button>().onClick.AddListener(delegate { ChangeSnowball.Run(gameObject, currentNumberSnowball); });

    }
}
