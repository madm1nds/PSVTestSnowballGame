using UnityEngine;
/// <summary>
/// Основные параметры каждого персонажа в игре (Hippo и противников)
/// speedCharacter - скорость каждого персонажа
/// speedCooldown - скорость перезарядки или скорость стрельбы
/// speedSnowball - скорость атакующей способности для каждого противника (для Hippo это значение константа)
/// </summary>
[CreateAssetMenu(menuName = "Settings/Character")]
public class Charactrer : ScriptableObject
{
    public float speedCharacter;
    public float speedCooldown;
    public float speedSnowball;
}
