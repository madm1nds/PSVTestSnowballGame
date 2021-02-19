using UnityEngine;
/// <summary>
/// Инициализация игры. 
/// Применяет стандартные настройки для отображения снежков.
/// Применяет перспективу 2.5D в случае активации соответствующей настройки.
/// Применяет все настройки. 
/// </summary>
public class InitSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject MainGround;
    [SerializeField]
    private GameObject groundFence;
    public static int healthPoints;
    private const int currentSnowballView = 0;

    void Start()
    {
        ChangeSnowball.Run(Vault.instance.buttonUIItemsMenu[currentSnowballView].gameObject, currentSnowballView);

        if (!Vault.instance.settings.mode_2_5D)
        {
            MainGround.transform.rotation = new Quaternion(0, 0, 0, 0);
            groundFence.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        Vault.instance.settings.ApplySettings();

        Vault.currentLanguage = Application.systemLanguage;
        
        SettingsMenu.SetLanguage(Vault.currentLanguage);

    }
}
