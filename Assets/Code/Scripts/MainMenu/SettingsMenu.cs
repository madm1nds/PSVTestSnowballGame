using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Добавляет логику для кнопок в настройках.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    private const float minValue = 0f;
    private const float maxValue = 1f;
    private const float delayBeforeInitialization = 0.02f;
    /// <summary>
    /// Инициализация все кнопок в настройках.
    /// </summary>
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeInitialization);
        Vault.instance.buttonUIMusic.onClick.AddListener(delegate { ChangeStateMusic(); });
        Vault.instance.buttonUISounds.onClick.AddListener(delegate { ChangeStateSounds(); });
        Vault.instance.buttonUIEffects.onClick.AddListener(delegate { ChangeStateEffects(); });

        Vault.instance.buttonUIRussian.onClick.AddListener(delegate { SetLanguage(SystemLanguage.Russian); });
        Vault.instance.buttonUIEnglish.onClick.AddListener(delegate { SetLanguage(SystemLanguage.English); });
        Vault.instance.buttonUIJapanese.onClick.AddListener(delegate { SetLanguage(SystemLanguage.Japanese); });

        if (Vault.instance.isMusic == false)
        {
            Vault.instance.isMusic = !Vault.instance.isMusic;
            ChangeStateMusic();
        }

        if (Vault.instance.isSounds == false)
        {
            Vault.instance.isSounds = !Vault.instance.isSounds;
            ChangeStateSounds();
        }


        if (Vault.instance.isEffects == false)
        {
            Vault.instance.isEffects = !Vault.instance.isEffects;
            ChangeStateEffects();
        }

    }
    /// <summary>
    /// Метод, который включает или выключает музыку.
    /// </summary>
    void ChangeStateMusic()
    {
        if (Vault.instance.isMusic)
        {
            Vault.instance.imageUIMusic.sprite = Vault.instance.spriteMusicOff;
            Vault.instance.audioSourceGameMusic.volume = minValue;
        }
        else
        {
            Vault.instance.imageUIMusic.sprite = Vault.instance.spriteMusicOn;
            Vault.instance.audioSourceGameMusic.volume = maxValue;
        }
        Vault.instance.isMusic = !Vault.instance.isMusic;
        Vault.instance.audioSourcePressButton.Play();
    }
    /// <summary>
    /// Метод, который включает или выключает все звуки в игре.
    /// </summary>
    void ChangeStateSounds()
    {
        if (Vault.instance.isSounds)
        {
            Vault.instance.imageUISounds.sprite = Vault.instance.spriteSoundsOff;
            Vault.instance.audioSourcePressButton.volume = minValue;
            Vault.instance.audioSourceWinFail.volume = minValue;
            for (int i = 0; i < Vault.instance.audioSourceHit.Length; i++)
            {
                Vault.instance.audioSourceHit[i].volume = minValue;
            }
            for (int i = 0; i < Vault.instance.audioSourceThrow.Length; i++)
            {
                Vault.instance.audioSourceThrow[i].volume = minValue;
            }
        }
        else
        {
            Vault.instance.audioSourcePressButton.Play();
            Vault.instance.imageUISounds.sprite = Vault.instance.spriteSoundsOn;
            Vault.instance.audioSourcePressButton.volume = maxValue;
            Vault.instance.audioSourceWinFail.volume = maxValue;
            for (int i = 0; i < Vault.instance.audioSourceHit.Length; i++)
            {
                Vault.instance.audioSourceHit[i].volume = maxValue;
            }
            for (int i = 0; i < Vault.instance.audioSourceThrow.Length; i++)
            {
                Vault.instance.audioSourceThrow[i].volume = maxValue;
            }
        }
        Vault.instance.isSounds = !Vault.instance.isSounds;
    }
    /// <summary>
    /// Метод, который включает или выключает частицы снега в игре.
    /// </summary>
    void ChangeStateEffects()
    {
        if (Vault.instance.isEffects)
        {
            Vault.instance.imageUIEffects.sprite = Vault.instance.spriteEffectsOff;
            Vault.instance.particleSystemSnow.maxParticles = 0;
        }
        else
        {
            Vault.instance.imageUIEffects.sprite = Vault.instance.spriteEffectsOn;
            Vault.instance.particleSystemSnow.maxParticles = 15;
        }
        Vault.instance.isEffects = !Vault.instance.isEffects;
        Vault.instance.audioSourcePressButton.Play();
    }
    /// <summary>
    /// Изменяет язык, на указанный в аргументе.
    /// </summary>
    /// <param name="newLanguage">Название языка, на который требуется изменить</param>
    public static void SetLanguage(SystemLanguage newLanguage)
    {
        Vault.currentLanguage = newLanguage;
        Vault.instance.buttonUIRussian.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 1f);
        Vault.instance.buttonUIEnglish.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 1f);
        Vault.instance.buttonUIJapanese.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 1f);
        switch (newLanguage)
        {
            case SystemLanguage.Russian:
                Vault.instance.buttonUIRussian.GetComponent<Image>().color = new Vector4(96 / 255f, 216 / 255f, 1f, 1f);
                break;
            case SystemLanguage.English:
                Vault.instance.buttonUIEnglish.GetComponent<Image>().color = new Vector4(96 / 255f, 216 / 255f, 1f, 1f);
                break;
            case SystemLanguage.Japanese:
                Vault.instance.buttonUIJapanese.GetComponent<Image>().color = new Vector4(96 / 255f, 216 / 255f, 1f, 1f);
                break;

        }

        Vault.instance.spriteRendererTextVictoryBoard.sprite = LanguageController.ChangeLanguage(SpriteName.Excellent);
        Vault.instance.imageUIEvasionButton.sprite = LanguageController.ChangeLanguage(SpriteName.EvasionMode);
        Vault.instance.imageUIThrowPower.sprite = LanguageController.ChangeLanguage(SpriteName.ThrowPower);
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
