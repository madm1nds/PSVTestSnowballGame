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
                PauseButtonController.instance.clickOnPause();
                break;
            case NameAnimation.ResetLevel:
                VictoryBoard.SetActive(false);
                PauseButtonController.instance.clickOnPause();
                HippoReset.Run();
                //TODO: Сделать выбор!
                EnemyStartLocation.instance.ResetLocation(1);
                break;
            case NameAnimation.ShowItemsMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectItemsMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                break;
            case NameAnimation.ShowStartGameMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectStartGameMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                break;
            case NameAnimation.ShowSettingsMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(false);
                Vault.instance.gameObjectSettingsMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                break;
            case NameAnimation.ShowMainMenu:
                Vault.instance.gameObjectMainMenuVaultButton.SetActive(true);
                Vault.instance.gameObjectItemsMenu.SetActive(false);
                Vault.instance.gameObjectStartGameMenu.SetActive(false);
                Vault.instance.gameObjectSettingsMenu.SetActive(false);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(false);
                break;
            case NameAnimation.ShowGameLevel:
                StartCoroutine(ShowGameLevel());
                break;
            case NameAnimation.SelectLevel:
                Vault.instance.gameObjectStartGameMenu.SetActive(true);
                Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(true);
                Vault.instance.gameObjectMainMenuUI.SetActive(true);
                StartCoroutine(ShowSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
                //Vault.instance.gameObjectMainMenu2D.SetActive(true);
                Vault.instance.gameObjectGameLevelUI.SetActive(false);
                Vault.instance.gameObjectGround[LevelNumberButton.currentNumberLevel - 1].SetActive(false);

                VictoryBoard.SetActive(false);

                break;
        }
        currentNameAnimation = NameAnimation.NoAnimation;       
    }
    IEnumerator ShowGameLevel()
    {
        Vault.instance.gameObjectStartGameMenu.SetActive(false);
        Vault.instance.buttonUIBackButton.transform.parent.gameObject.gameObject.SetActive(false);
        Vault.instance.gameObjectMainMenuUI.SetActive(false);
        StartCoroutine(HideSpriteRendererObject(Vault.instance.spriteRendererMainMenuBackground));
        Vault.instance.gameObjectGround[LevelNumberButton.currentNumberLevel - 1].SetActive(true);
        HippoReset.Run();
        EnemyStartLocation.instance.ResetLocation(LevelNumberButton.currentNumberLevel - 1);
        //Vault.instance.gameObjectMainMenu2D.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        Vault.instance.gameObjectGameLevelUI.SetActive(true);
        PauseButtonController.instance.clickOnPause();
        VictoryBoard.SetActive(false);

        yield break;
    }
    public IEnumerator HideSpriteRendererObject(SpriteRenderer sprite)
    {
        const float speedChange = 0.05f;
        do
        {
            sprite.color = new Vector4(1, 1, 1, sprite.color.a - speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        sprite.gameObject.SetActive(false);
        yield break;
    }
    public IEnumerator ShowSpriteRendererObject(SpriteRenderer sprite)
    {
        const float speedChange = 0.05f;
        sprite.gameObject.SetActive(true);
        do
        {
            sprite.color = new Vector4(1, 1, 1, sprite.color.a + speedChange*3);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        yield break;
    }
    public IEnumerator HideSpriteObject(Image sprite)
    {
        const float speedChange = 0.05f;
        do
        {
            sprite.color = new Vector4(1, 1, 1, sprite.color.a - speedChange);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        sprite.gameObject.SetActive(false);
        yield break;
    }
    public IEnumerator ShowSpriteObject(Image sprite)
    {
        const float speedChange = 0.05f;
        sprite.gameObject.SetActive(true);
        do
        {
            sprite.color = new Vector4(1, 1, 1, sprite.color.a + speedChange * 3);
            yield return new WaitForSeconds(0.015f);
        } while (sprite.color.a > 0);
        yield break;
    }
}
