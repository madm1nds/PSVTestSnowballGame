using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Spine.Unity;
/// <summary>
/// Добавляет логику для кнопки "PauseButton" в игровом уровне.
/// Содержит метод для "заморозки" игрового пространства.
/// isPause - находится ли игра на паузе.
/// pauseButton - кнопка паузы.
/// screenLock - прозрачная картинка для блокирования интерфейса.
/// </summary>
public class PauseButtonController : MonoBehaviour
{
    public static bool isPause;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject screenLock;
    [SerializeField]

    public static PauseButtonController instance;

    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<PauseButtonController>();
        }

        isPause = false;
        pauseButton.onClick.AddListener(delegate { clickOnPause(); });
        clickOnPause();
        Vault.instance.gameObjectVictoryBoard.SetActive(false);
    }
    /// <summary>
    /// Включает паузу. Меняет состояние isPause.
    /// </summary>
    public void clickOnPause()
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
            Vault.instance.spriteRendererTextVictoryBoard.sprite = Vault.instance.spritePauseRus;
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
            do
            {
                for (int i = 0; i < Vault.instance.imageGameLevelUI.Length; i++)
                {
                    Vault.instance.imageGameLevelUI[i].color = new Vector4(1, 1, 1, Vault.instance.imageGameLevelUI[i].color.a - speedChange);
                }
                yield return new WaitForSeconds(0.015f);
            } while (Vault.instance.imageGameLevelUI[0].color.a > 0);
        }
        else
        {
            do
            {
                for (int i = 0; i < Vault.instance.imageGameLevelUI.Length; i++)
                {
                    Vault.instance.imageGameLevelUI[i].color = new Vector4(1, 1, 1, Vault.instance.imageGameLevelUI[i].color.a + speedChange*3);
                }
                yield return new WaitForSeconds(0.015f);
            } while (Vault.instance.imageGameLevelUI[0].color.a < 1);
        }
        yield break;
    }
}
