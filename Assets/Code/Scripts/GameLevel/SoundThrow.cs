using UnityEngine;
/// <summary>
/// Класс, который случайный образом выбирает звук броска и включает доступный AudioSource
/// </summary>
public static class SoundThrow
{
    /// <summary>
    /// Включить доступный AudioSource со случайным звком.
    /// </summary>
    /// <param name="audioSource">Массив заготовленных объектов AudioSource на сцене.</param>
    public static void Run(AudioSource[] audioSource, AudioClip[] audioClip)
    {
        int randomSound = Random.Range(0, audioClip.Length);
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].isPlaying == false)
            {
                audioSource[i].clip = audioClip[randomSound];
                audioSource[i].Play();
                break;
            }
        }
    }
}
