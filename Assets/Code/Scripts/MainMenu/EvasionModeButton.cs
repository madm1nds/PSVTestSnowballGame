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
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { RunLevel(); });
    }
    /// <summary>
    /// Запуск уклоняшек.
    /// </summary>
    void RunLevel()
    {
        AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ShowGameLevel;
        LevelNumberButton.currentNumberLevel = Random.Range(1, 6);
        for (int i = 0; i < Vault.instance.gameObjectsStartGameMenu.Length; i++)
        {
            Vault.instance.gameObjectsStartGameMenu[i].GetComponent<Animator>().SetTrigger("Exit");
        }
        Vault.instance.settings.evasionMode = true;
        Vault.instance.settings.isMove_y = false;
        Vault.instance.joystickUIJoystick.GetComponent<SimpleInputNamespace.Joystick>().movementAxes = SimpleInputNamespace.Joystick.MovementAxes.XandY;
        Vault.instance.settings.ApplySettings();
    }
}
