using UnityEngine;

public static class LanguageController
{
    /// <summary>
    /// Изменяет язык спрайта. 
    /// </summary>
    /// <param name="spriteName">Название спрайта, язык которого будет изменён.</param>
    /// <returns></returns>
    public static Sprite ChangeLanguage(SpriteName spriteName)
    {
        switch (Vault.currentLanguage)
        {
            case SystemLanguage.Russian:

                if (spriteName == SpriteName.EvasionMode)
                    return Vault.instance.spriteEvasionModeRus;
                if (spriteName == SpriteName.Excellent)
                    return Vault.instance.spriteExcellentRus;
                if (spriteName == SpriteName.Fiasco)
                    return Vault.instance.spriteFiascoRus;
                if (spriteName == SpriteName.Pause)
                    return Vault.instance.spritePauseRus;
                if (spriteName == SpriteName.ThrowPower)
                    return Vault.instance.spriteThrowPowerRus;
                break;

            case SystemLanguage.English:

                if (spriteName == SpriteName.EvasionMode)
                    return Vault.instance.spriteEvasionModeEng;
                if (spriteName == SpriteName.Excellent)
                    return Vault.instance.spriteExcellentEng;
                if (spriteName == SpriteName.Fiasco)
                    return Vault.instance.spriteFiascoEng;
                if (spriteName == SpriteName.Pause)
                    return Vault.instance.spritePauseEng;
                if (spriteName == SpriteName.ThrowPower)
                    return Vault.instance.spriteThrowPowerEng;
                break;

            case SystemLanguage.Japanese:

                if (spriteName == SpriteName.EvasionMode)
                    return Vault.instance.spriteEvasionModeJap;
                if (spriteName == SpriteName.Excellent)
                    return Vault.instance.spriteExcellentJap;
                if (spriteName == SpriteName.Fiasco)
                    return Vault.instance.spriteFiascoJap;
                if (spriteName == SpriteName.Pause)
                    return Vault.instance.spritePauseJap;
                if (spriteName == SpriteName.ThrowPower)
                    return Vault.instance.spriteThrowPowerJap;
                break;
        }
        return null;
    }
}
