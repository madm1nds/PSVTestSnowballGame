using UnityEngine;
[CreateAssetMenu(menuName = "Settings/StoppingEnemy")]
public class StoppingEnemy : ScriptableObject
{
    [Range(0f, 1f)]
    public float chance = 0.3f;
    public float time = 3f;
}
