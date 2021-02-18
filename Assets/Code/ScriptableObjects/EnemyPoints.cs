using UnityEngine;
/// <summary>
/// Количество очков, которые игрок получит при попадании в противника.
/// </summary>
[CreateAssetMenu(menuName = "Settings/EnemyPoints")]
public class EnemyPoints : ScriptableObject
{
    public int enemyPoints;
}
