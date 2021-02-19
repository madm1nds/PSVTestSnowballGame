using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Сбрасывает все значения Hippo. Возвращает его на начальную позицию и возвращает начальные размеры.
/// Восстанавливает отображения невредимых сердец, все звёзды в VictoryBoard и систему частиц, количество жизней.
/// Сбрасывает счёт игрока, время раунда, время силы броска и перезарядки Hippo.
/// defaultScale - стандартные размеры Hippo.
/// </summary>
public static class HippoReset
{
    private const float defaultScale = 0.6f;
    private const float defaultPosition_x = -5.25f;
    private const float defaultPosition_y = -1f;
    private const float defaultPosition_z = -4f;

    private const int healthPoints = 3;
    private const int initialScorePlayers = 0;
    private const float gravityValue = 0f;
    private const int initialTime = -1;
    private const int initialPoints = 0;
    private const int sliderPostion = 0;

    /// <summary>
    /// Запуск сброса.
    /// </summary>
    public static void Run()
    {
        for (int i = 0; i < Vault.instance.gameObjectHippoSnowballSet.Length; i++)
        {
            if (Vault.instance.gameObjectHippoSnowballSet[i].activeInHierarchy == true)
            {
                Vault.instance.gameObjectHippoSnowballSet[i].SetActive(false);
            }
        }
        Vault.instance.gameObjectHippo.transform.position = new Vector3(defaultPosition_x, defaultPosition_y, defaultPosition_z);
        Vault.instance.gameObjectHippo.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        Vault.instance.imageUIHearts[0].sprite = Vault.instance.spriteGoodHeart;
        Vault.instance.imageUIHearts[1].sprite = Vault.instance.spriteGoodHeart;
        Vault.instance.imageUIHearts[2].sprite = Vault.instance.spriteGoodHeart;

        Vault.instance.spriteRendererStarLeft.sprite = Vault.instance.spriteStarLeftOn;
        Vault.instance.spriteRendererStarCenter.sprite = Vault.instance.spriteStarCenterOn;
        Vault.instance.spriteRendererStarRight.sprite = Vault.instance.spriteStarRightOn;
        InitSettings.healthPoints = healthPoints;
        ScoreSetController.scorePlayer = initialScorePlayers;
        TimerRoundController.currentTime = initialTime;
        ScoreSetController.instance.RefreshPoints(initialPoints);
        Vault.instance.sliderUISlider.GetComponent<Slider>().value = sliderPostion;
        Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition = new Vector3(0f, Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition.y,
                                        Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition.z);
        if (Vault.instance.gameObjectHippoReadySnowball.activeInHierarchy == true)
        {
            StatusBarAbilityController.instance.InvokeChangeStatus();
        }
        Vault.instance.gameObjectHippo.SetActive(true);

        Vault.instance.particleSystemVictoryBoard.gravityModifier = gravityValue;
    }
}
