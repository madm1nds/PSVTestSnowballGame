using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// В зависимости от текущего состояния анимации игровые объекты включаются и выключаются.
/// Состояние анимации изменяется извне. 
/// Окончание анимации определяется при помощи Transitions.cs, который наследуется от StateMachineBehaviour
/// Каждая анимация содержит в аниматоре данный скрипт. При завершении анимации StateMachineBehaviour
/// фиксирует окончание и запускает метод Run().
/// 
/// Поле "instance" - ссылка на самого себя. Необходим, для доступа к открытым полям и методам.
/// </summary>
public class AnimationActions : MonoBehaviour
{
    [SerializeField]
    private GameObject VictoryBoard;
    /// <summary>
    /// Перечесление состояний анимации. Применяется в AnimationActions.cs
    /// </summary>
    public enum NameAnimation
    {
        NoAnimation,
        ShowItemsMenu,
        ShowStartGameMenu,
        ShowSettingsMenu,
        ShowMainMenu,
        ShowGameLevel,
        TurnOffPause,
        ResetLevel,
        SelectLevel

    }
    public static NameAnimation currentNameAnimation;
    public static AnimationActions instance;
    private void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<AnimationActions>();
        }
    }
    /// <summary>
    /// В зависимости от состояния анимации, запускается определённое поведение.
    /// </summary>
    public void Run()
    {
        switch (currentNameAnimation)
        {
            case NameAnimation.TurnOffPause:
                VictoryBoard.SetActive(false);
                StartCoroutine(HideSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
                PauseButtonController.instance.ClickOnPause();
                break;
            case NameAnimation.ResetLevel:
                VictoryBoard.SetActive(false);
                StartCoroutine(HideSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
                PauseButtonController.instance.ClickOnPause();
                HippoReset.Run();

                StartCoroutine(EnemyStartLocation.instance.ResetLocation(LevelNumberButton.currentNumberLevel - 1));
                break;
            case NameAnimation.ShowItemsMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectItemsMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                for (int i = 0; i < Vault.instance.imageItemsMenu.Length; i++)
                {
                    StartCoroutine(ShowSpriteObject(Vault.instance.imageItemsMenu[i]));
                }
                break;
            case NameAnimation.ShowStartGameMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectStartGameMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                for (int i = 0; i < Vault.instance.imageStartGameMenu.Length; i++)
                {
                    StartCoroutine(ShowSpriteObject(Vault.instance.imageStartGameMenu[i]));
                }
                break;
            case NameAnimation.ShowSettingsMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectSettingsMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                for (int i = 0; i < Vault.instance.imageSettingsMenu.Length; i++)
                {
                    StartCoroutine(ShowSpriteObject(Vault.instance.imageSettingsMenu[i]));
                }
                break;
            case NameAnimation.ShowMainMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(true);
                Vault.instance.gameObjectItemsMenu.SetActive(false);
                Vault.instance.gameObjectStartGameMenu.SetActive(false);
                Vault.instance.gameObjectSettingsMenu.SetActive(false);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(false);
                for (int i = 0; i < Vault.instance.imageMainMenuVaultButton.Length; i++)
                {
                    StartCoroutine(ShowSpriteObject(Vault.instance.imageMainMenuVaultButton[i]));
                }
                break;
            case NameAnimation.ShowGameLevel:
                Vault.instance.gameObjectStartGameMenu.SetActive(false);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(false);
                Vault.instance.gameObjectMainMenuUI.SetActive(false);
                Vault.instance.gameObjectGround[LevelNumberButton.currentNumberLevel - 1].SetActive(true);
                HippoReset.Run();
                StartCoroutine(EnemyStartLocation.instance.ResetLocation(LevelNumberButton.currentNumberLevel - 1));
                StartCoroutine(HideSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));

                Vault.instance.gameObjectGameLevelUI.SetActive(true);
                PauseButtonController.instance.ClickOnPause();
                VictoryBoard.SetActive(false);
                break;
            case NameAnimation.SelectLevel:
                Vault.instance.gameObjectStartGameMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                Vault.instance.gameObjectMainMenuUI.SetActive(true);
                //StartCoroutine(ShowSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
                for (int i = 0; i < Vault.instance.imageStartGameMenu.Length; i++)
                {
                    StartCoroutine(ShowSpriteObject(Vault.instance.imageStartGameMenu[i]));
                }

                Vault.instance.gameObjectGameLevelUI.SetActive(false);
                Vault.instance.gameObjectGround[LevelNumberButton.currentNumberLevel - 1].SetActive(false);

                VictoryBoard.SetActive(false);

                break;
        }
        currentNameAnimation = NameAnimation.NoAnimation;
    }
    /// <summary>
    /// Плавно уменьшает альфа канал у SpriteRenderer до нуля.
    /// </summary>
    /// <param name="sprite">SpriteRenderer, у которого будет меняться прозрачность.</param>
    /// <returns></returns>
    public IEnumerator HideSpriteRendererObject(SpriteRenderer sprite)
    {
        const float speedChange = 0.05f;
        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a);
        do
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        sprite.gameObject.SetActive(false);
        yield break;
    }
    /// <summary>
    /// Плавно увеличивает альфа канал у SpriteRenderer до 1.
    /// </summary>
    /// <param name="sprite">SpriteRenderer, у которого будет меняться прозрачность.</param>
    /// <returns></returns>
    public IEnumerator ShowSpriteRendererObject(SpriteRenderer sprite)
    {
        const float speedChange = 0.05f;

        float pastAlpha = sprite.color.a <= 0.001f ? 1 : sprite.color.a;
        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        sprite.gameObject.SetActive(true);
        do
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a < pastAlpha);
        yield break;
    }
    /// <summary>
    /// Плавно уменьшает альфа канал в Image до 0.
    /// </summary>
    /// <param name="sprite">Image, у которого будет меняться прозрачность.</param>
    /// <returns></returns>
    public IEnumerator HideSpriteObject(Image sprite)
    {
        const float speedChange = 0.05f;
        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a);
        do
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        sprite.gameObject.SetActive(false);
        yield break;
    }
    /// <summary>
    /// Плавно увеличивает альфа канал в Image до 1.
    /// </summary>
    /// <param name="sprite">Image, у которого будет меняться прозрачность.</param>
    /// <returns></returns>
    public IEnumerator ShowSpriteObject(Image sprite)
    {
        const float speedChange = 0.05f;
        float pastAlpha = sprite.color.a <= 0.001f ? 1 : sprite.color.a;
        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        sprite.gameObject.SetActive(true);
        do
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a < pastAlpha);
        yield break;
    }
}
