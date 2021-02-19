using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWinFail : MonoBehaviour
{
    public static IEnumerator Run(AudioClip stateGameClip)
    {
        float initialVolumeGameMusic = Vault.instance.audioSourceGameMusic.volume;
        Vault.instance.audioSourceWinFail.clip = stateGameClip;
        Vault.instance.audioSourceWinFail.Play();
        
        for (int i = 0; i < 18; i++)
        {
            Vault.instance.audioSourceGameMusic.volume -= 0.04f;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(stateGameClip.length-1);
        do
        {
            Vault.instance.audioSourceGameMusic.volume += 0.04f;
            yield return new WaitForSeconds(0.03f);
        } while (Vault.instance.audioSourceGameMusic.volume < initialVolumeGameMusic);
        yield break;
    }
}
