using UnityEngine;

/// <summary>
/// Основные параметры каждого персонажа в игре (Hippo и противников)
/// speedCharacter - скорость каждого персонажа
/// speedCooldown - скорость перезарядки или скорость стрельбы
/// </summary>
[CreateAssetMenu(menuName = "Settings/Character")]
public class Charactrer : ScriptableObject
{
    public float speedCharacter;
    public float speedCooldown;
}
