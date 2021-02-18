using UnityEngine;
/// <summary>
/// Определяет вероятность остановки противников и время остановки противников.
/// </summary>
[CreateAssetMenu(menuName = "Settings/StoppingEnemy")]
public class StoppingEnemy : ScriptableObject
{
    [Range(0f, 1f)]
    public float chance = 0.3f;
    public float time = 3f;
}
