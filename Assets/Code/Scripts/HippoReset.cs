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
    const float defaultScale = 0.6f;
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
        Vault.instance.gameObjectHippo.transform.position = new Vector3(-5.25f, -1, -4);
        Vault.instance.gameObjectHippo.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        Vault.instance.imageUIHearts[0].sprite = Vault.instance.spriteGoodHeart;
        Vault.instance.imageUIHearts[1].sprite = Vault.instance.spriteGoodHeart;
        Vault.instance.imageUIHearts[2].sprite = Vault.instance.spriteGoodHeart;

        Vault.instance.spriteRendererStarLeft.sprite = Vault.instance.spriteStarLeftOn;
        Vault.instance.spriteRendererStarCenter.sprite = Vault.instance.spriteStarCenterOn;
        Vault.instance.spriteRendererStarRight.sprite = Vault.instance.spriteStarRightOn;
        InitSettings.healthPoints = 30;
        ScoreSetController.scorePlayer = 0;
        TimerRoundController.currentTime = -1;
        ScoreSetController.instance.RefreshPoints(0);
        Vault.instance.sliderUISlider.GetComponent<Slider>().value = 0;
        Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition = new Vector3(0f, Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition.y,
                                        Vault.instance.gameObjectStatusBarAbilityMask.transform.localPosition.z);
        if (Vault.instance.gameObjectHippoReadySnowball.activeInHierarchy == true)
        {
            StatusBarAbilityController.instance.InvokeChangeStatus();
        }
        Vault.instance.gameObjectHippo.SetActive(true);

        Vault.instance.particleSystemVictoryBoard.gravityModifier = 0;
    }
}
