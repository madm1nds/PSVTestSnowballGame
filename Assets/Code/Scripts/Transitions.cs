using UnityEngine;
/// <summary>
///  Находится в каждом аниматоре. Запускается при завершении анимаций.
/// </summary>
public class Transitions : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimationActions.instance.Run();
    }
}
