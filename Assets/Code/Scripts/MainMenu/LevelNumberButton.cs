using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Добавляет логику для кнопок "Level_1", "Level_...", "Level_N" в выборе уровней (StartGameMenu)
/// Содержит открытое статическое поле "currentNumberLevel", которое указывает, какой уровень выбран в игре
/// numberLevel - локальное поле для каждой кнопки. Определяет номер уровня. Задаётся в инспекторе.
/// </summary>
public class LevelNumberButton : MonoBehaviour
{
    public static int currentNumberLevel;
    [SerializeField]
    private int numberLevel;
    private const float delayBeforeInitialization = 0.2f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeInitialization);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { RunLevel(); });
    }
    /// <summary>
    /// Активирует переход в уровень, после завершения анимации.
    /// </summary>
    void RunLevel()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowGameLevel;
        currentNumberLevel = numberLevel;
        Vault.instance.settings.evasionMode = false;
        Vault.instance.settings.isMove_y = true;
        Vault.instance.joystickUIJoystick.GetComponent<SimpleInputNamespace.Joystick>().movementAxes = SimpleInputNamespace.Joystick.MovementAxes.Y;
        Vault.instance.imageUIThumbJoystick.sprite = Vault.instance.spriteThumbJoystickY;
        Vault.instance.settings.ApplySettings();
        for (int i = 0; i < Vault.instance.gameObjectsStartGameMenu.Length; i++)
        {
            Vault.instance.gameObjectsStartGameMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
        Vault.instance.audioSourcePressButton.Play();
    }
}
