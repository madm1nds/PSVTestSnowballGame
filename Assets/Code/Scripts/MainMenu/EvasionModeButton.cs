using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Включает случайный уровень в игре.
/// Включается режим уклоняшек! У каждого противника своя собственная перезарядка!
/// У игрока появляется возможность передвигаться по любой оси!
/// </summary>
public class EvasionModeButton : MonoBehaviour
{
    private const float delayBeforeInitialization = 0.2f;
    private const int minNumberLevel = 1;
    private const int maxNumberLevel = 5;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeInitialization);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { RunLevel(); });
    }
    /// <summary>
    /// Запуск уклоняшек.
    /// </summary>
    void RunLevel()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowGameLevel;
        LevelNumberButton.currentNumberLevel = Random.Range(minNumberLevel, maxNumberLevel + 1);
        for (int i = 0; i < Vault.instance.gameObjectsStartGameMenu.Length; i++)
        {
            Vault.instance.gameObjectsStartGameMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
        Vault.instance.settings.evasionMode = true;
        Vault.instance.settings.isMove_y = false;
        Vault.instance.joystickUIJoystick.GetComponent<SimpleInputNamespace.Joystick>().movementAxes = SimpleInputNamespace.Joystick.MovementAxes.XandY;
        Vault.instance.imageUIThumbJoystick.sprite = Vault.instance.spriteThumbJoystickXY;
        Vault.instance.settings.ApplySettings();
        Vault.instance.audioSourcePressButton.Play();
    }
}
