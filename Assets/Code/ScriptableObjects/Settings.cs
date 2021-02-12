using UnityEngine;
[CreateAssetMenu(menuName = "AllSettings")]
public class Settings : ScriptableObject
{
    [Header("Скорость передвижения Hippo")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedHippo;
    [Header("Скорость перезарядки Hippo")]
    public float speedCooldownHippo;

    [Header("Количество очков для победы")]
    [Header("-----------------------------------------------------------------------------")]
    public int pointsForVictory;

    [Header("Очки за попадания по противнику")]   
    public int pointsPumaPapa;
    public int pointsFoxSon;
    public int pointsRaccoonPapa;

    [Header("Скорость передвижения PumaPapa")]
    [Header("Свойства противников")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedPumaPapa;
    [Header("Перезарядка PumaPapa (для режима \"Уклоняшки\")")]
    public float speedCooldownPumaPapa;

    [Header("Скорость передвижения FoxSon")]
    public float speedFoxSon;
    [Header("Перезарядка FoxSon (для режима \"Уклоняшки\")")]
    public float speedCooldownFoxSon;

    [Header("Скорость передвижения RaccoonPapa")]
    public float speedRaccoonPapa;
    [Header("Перезарядка RaccoonPapa (для режима \"Уклоняшки\")")]
    public float speedCooldownRaccoonPapa;

    [Header("Вероятность остановки противника")]
    [Header("-----------------------------------------------------------------------------")]
    [Range(0f, 1f)]
    public float chanceStop = 0.1f;
    [Header("Время остановки противника")]
    public float timeStop = 2f;

    [Header("-------------------- Для разработчиков --------------------")]
    [Header("")]
    [Header("")]
    [SerializeField]
    private EnemyPoints[] points;
    [SerializeField]
    private Charactrer[] enemyCharacters;
    [SerializeField]
    private StoppingEnemy stoppingEnemy;
    [SerializeField]
    private Charactrer hippo;
    [SerializeField]
    private Snowball cooldownHippo;
    [SerializeField]
    private RequiredPoints requiredPoints;

    public void ApplySettings()
    {
        points[0].enemyPoints = pointsPumaPapa;
        points[1].enemyPoints = pointsFoxSon;
        points[2].enemyPoints = pointsRaccoonPapa;

        enemyCharacters[0].speedCharacter = speedPumaPapa;
        enemyCharacters[0].speedCooldown = speedCooldownPumaPapa;
        enemyCharacters[1].speedCharacter = speedFoxSon;
        enemyCharacters[1].speedCooldown = speedCooldownFoxSon;
        enemyCharacters[2].speedCharacter = speedRaccoonPapa;
        enemyCharacters[2].speedCooldown = speedCooldownRaccoonPapa;

        stoppingEnemy.chance = chanceStop;
        stoppingEnemy.time = timeStop;

        hippo.speedCharacter = speedHippo;
        hippo.speedCooldown = speedCooldownHippo;
        cooldownHippo.cooldown = speedCooldownHippo;

        requiredPoints.points = pointsForVictory;


    }
}
