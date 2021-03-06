﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;
/// <summary>
/// Добавляет логику для кнопки "PauseButton" в игровом уровне.
/// Содержит метод для "заморозки" игрового пространства.
/// isPause - находится ли игра на паузе.
/// pauseButton - кнопка паузы.
/// screenLock - прозрачная картинка для блокирования интерфейса.
/// isStart - игра была только что запущена.
/// </summary>
public class PauseButtonController : MonoBehaviour
{
    public static bool isPause;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject screenLock;

    public static PauseButtonController instance;
    private static bool isStart;
    private const int minAlpha = 0;
    private const int maxAlpha = 1;

    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<PauseButtonController>();
        }
        isStart = true;
        isPause = false;
        pauseButton.onClick.AddListener(delegate { ClickOnPause(); });
        ClickOnPause();
        Vault.instance.gameObjectVictoryBoard.SetActive(false);
    }
    /// <summary>
    /// Включает паузу. Меняет состояние isPause.
    /// </summary>
    public void ClickOnPause()
    {

        if (isPause == false)
        {
            for (int i = 0; i < Vault.instance.skeletonAnimationEnemies.Length; i++)
            {
                if (Vault.instance.skeletonAnimationEnemies[i].gameObject.activeInHierarchy == true)
                {
                    Vault.instance.skeletonAnimationEnemies[i].AnimationName = "Idle";
                }
            }
            Vault.instance.gameObjectVictoryBoard.SetActive(true);
            Vault.instance.gameObjectVictoryBoardRunLevel.SetActive(true);
            Vault.instance.gameObjectStarLeft.SetActive(false);
            Vault.instance.gameObjectStarCenter.SetActive(false);
            Vault.instance.gameObjectStarRight.SetActive(false);
            Vault.instance.spriteRendererTextVictoryBoard.sprite = LanguageController.ChangeLanguage(SpriteName.Pause);//Vault.instance.spritePauseRus;
            screenLock.SetActive(true);
        }
        else
        {
            screenLock.SetActive(false);
        }
        StartCoroutine(ChangeTransparent(isPause));
        isPause = !isPause;
    }
    /// <summary>
    /// В зависимости от состояния паузы постепенно делает видимым/невидимым интерфейс.
    /// </summary>
    /// <param name="isPause">Состояние паузы</param>
    IEnumerator ChangeTransparent(bool isPause)
    {
        const float speedChange = 0.05f;
        if (isPause == false)
        {
            if (isStart == false)
            {
                StartCoroutine(AnimationActions.instance.ShowSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
            }
            do
            {

                for (int i = 0; i < Vault.instance.imageGameLevelUI.Length; i++)
                {
                    Vault.instance.imageGameLevelUI[i].color = new Vector4(1, 1, 1, Vault.instance.imageGameLevelUI[i].color.a - speedChange);
                }

                yield return new WaitForSeconds(0.015f);
            } while (Vault.instance.imageGameLevelUI[0].color.a > minAlpha);
        }
        else
        {
            do
            {
                for (int i = 0; i < Vault.instance.imageGameLevelUI.Length; i++)
                {
                    Vault.instance.imageGameLevelUI[i].color = new Vector4(1, 1, 1, Vault.instance.imageGameLevelUI[i].color.a + speedChange * 3);
                }
                yield return new WaitForSeconds(0.015f);
            } while (Vault.instance.imageGameLevelUI[0].color.a < maxAlpha);
        }
        isStart = false;
        yield break;
    }
}
