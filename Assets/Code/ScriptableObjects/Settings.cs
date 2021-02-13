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

    [Header("Режим уклоняшек")]
    [Header("-----------------------------------------------------------------------------")]
    public bool evasionMode;

    [Header("Скорость перезарядки в обычном режиме")]
    [Header("-----------------------------------------------------------------------------")]
    public float cooldownSpeedNormalMode;

    [Header("Очки за попадания по противнику")]   
    public int pointsPumaPapa;
    public int pointsPumaMama;
    public int pointsPumaDaughter;
    public int pointsFoxSon;
    public int pointsFoxPapa;
    public int pointsFoxGrandma;
    public int pointsRaccoonPapa;
    public int pointsRaccoonSon;
    public int pointsRaccoonGrandpa;

    [Header("Скорость передвижения PumaPapa")]
    [Header("Свойства противников")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedPumaPapa;
    [Header("Перезарядка PumaPapa (для режима \"Уклоняшки\")")]
    public float speedCooldownPumaPapa;

    [Header("Скорость передвижения PumaMama")]
    public float speedPumaMama;
    [Header("Перезарядка PumaMama (для режима \"Уклоняшки\")")]
    public float speedCooldownPumaMama;

    [Header("Скорость передвижения PumaDaughter")]
    public float speedPumaDaughter;
    [Header("Перезарядка PumaDaughter (для режима \"Уклоняшки\")")]
    public float speedCooldownPumaDaughter;

    [Header("Скорость передвижения FoxSon")]
    public float speedFoxSon;
    [Header("Перезарядка FoxSon (для режима \"Уклоняшки\")")]
    public float speedCooldownFoxSon;

    [Header("Скорость передвижения FoxPapa")]
    public float speedFoxPapa;
    [Header("Перезарядка FoxPapa (для режима \"Уклоняшки\")")]
    public float speedCooldownFoxPapa;

    [Header("Скорость передвижения FoxGrandma")]
    public float speedFoxGrandma;
    [Header("Перезарядка FoxGrandma (для режима \"Уклоняшки\")")]
    public float speedCooldownFoxGrandma;

    [Header("Скорость передвижения RaccoonPapa")]
    public float speedRaccoonPapa;
    [Header("Перезарядка RaccoonPapa (для режима \"Уклоняшки\")")]
    public float speedCooldownRaccoonPapa;

    [Header("Скорость передвижения RaccoonSon")]
    public float speedRaccoonSon;
    [Header("Перезарядка RaccoonSon (для режима \"Уклоняшки\")")]
    public float speedCooldownRaccoonSon;

    [Header("Скорость передвижения RaccoonGrandpa")]
    public float speedRaccoonGrandpa;
    [Header("Перезарядка RaccoonGrandpa (для режима \"Уклоняшки\")")]
    public float speedCooldownRaccoonGrandpa;

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
    private EnemyPoints pointsPumaPapaComponent;
    [SerializeField]
    private EnemyPoints pointsPumaMamaComponent;
    [SerializeField]
    private EnemyPoints pointsPumaDaughterComponent;
    [SerializeField]
    private EnemyPoints pointsFoxSonComponent;
    [SerializeField]
    private EnemyPoints pointsFoxPapaComponent;
    [SerializeField]
    private EnemyPoints pointsFoxGrandmaComponent;
    [SerializeField]
    private EnemyPoints pointsRaccoonPapaComponent;
    [SerializeField]
    private EnemyPoints pointsRaccoonSonComponent;
    [SerializeField]
    private EnemyPoints pointsRaccoonGrandpaComponent;
    [Header("----------------------------------------")]
    [SerializeField]
    private Charactrer pumaPapaCharacters;
    [SerializeField]
    private Charactrer pumaMamaCharacters;
    [SerializeField]
    private Charactrer pumaDaughterCharacters;
    [SerializeField]
    private Charactrer foxSonCharacters;
    [SerializeField]
    private Charactrer foxPapaCharacters;
    [SerializeField]
    private Charactrer foxGrandmaCharacters;
    [SerializeField]
    private Charactrer raccoonPapaCharacters;
    [SerializeField]
    private Charactrer raccoonSonCharacters;
    [SerializeField]
    private Charactrer raccoonGrandpaCharacters;
    [Header("----------------------------------------")]
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
        // Points
        pointsPumaPapaComponent.enemyPoints = pointsPumaPapa;
        pointsPumaMamaComponent.enemyPoints = pointsPumaMama;
        pointsPumaDaughterComponent.enemyPoints = pointsPumaDaughter;

        pointsFoxSonComponent.enemyPoints = pointsFoxSon;
        pointsFoxPapaComponent.enemyPoints = pointsFoxPapa;
        pointsFoxGrandmaComponent.enemyPoints = pointsFoxGrandma;

        pointsRaccoonPapaComponent.enemyPoints = pointsRaccoonPapa;
        pointsRaccoonSonComponent.enemyPoints = pointsRaccoonSon;
        pointsRaccoonGrandpaComponent.enemyPoints = pointsRaccoonGrandpa;

        // Chatacters
        pumaPapaCharacters.speedCharacter = speedPumaPapa;
        pumaPapaCharacters.speedCooldown = speedCooldownPumaPapa;

        pumaMamaCharacters.speedCharacter = speedPumaMama;
        pumaMamaCharacters.speedCooldown = speedCooldownPumaMama;

        pumaDaughterCharacters.speedCharacter = speedPumaDaughter;
        pumaDaughterCharacters.speedCooldown = speedCooldownPumaDaughter;

        foxSonCharacters.speedCharacter = speedFoxSon;
        foxSonCharacters.speedCooldown = speedCooldownFoxSon;

        foxPapaCharacters.speedCharacter = speedFoxPapa;
        foxPapaCharacters.speedCooldown = speedCooldownFoxPapa;

        foxGrandmaCharacters.speedCharacter = speedFoxGrandma;
        foxGrandmaCharacters.speedCooldown = speedCooldownFoxGrandma;

        raccoonPapaCharacters.speedCharacter = speedRaccoonPapa;
        raccoonPapaCharacters.speedCooldown = speedCooldownRaccoonPapa;

        raccoonSonCharacters.speedCharacter = speedRaccoonSon;
        raccoonSonCharacters.speedCooldown = speedCooldownRaccoonSon;

        raccoonGrandpaCharacters.speedCharacter = speedRaccoonGrandpa;
        raccoonGrandpaCharacters.speedCooldown = speedCooldownRaccoonGrandpa;

        // Other
        stoppingEnemy.chance = chanceStop;
        stoppingEnemy.time = timeStop;

        hippo.speedCharacter = speedHippo;
        hippo.speedCooldown = speedCooldownHippo;
        cooldownHippo.cooldown = speedCooldownHippo;

        requiredPoints.points = pointsForVictory;


    }
}
