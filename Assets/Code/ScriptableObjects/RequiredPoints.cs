using UnityEngine;
/// <summary>
/// Количество очков необходимых для победы.
/// </summary>
[CreateAssetMenu(menuName = "Settings/RequiredPoints")]
public class RequiredPoints : ScriptableObject
{
    public int points;
}
