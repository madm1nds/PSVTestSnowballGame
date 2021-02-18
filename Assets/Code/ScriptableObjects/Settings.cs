using UnityEngine;
/// <summary>
/// Основные настройки в игре. 
/// Содержит поля для общего использования и для разработчиков.
/// Поля для разработчиков служат для передачи новых значений выбранных в инспекторе в определённые Scriptable Object'ы
/// После передачи, все небольшие Scriptable Object'ы распределяются по определённым объектам на сцене./// 
/// </summary>
[CreateAssetMenu(menuName = "AllSettings")]
public class Settings : ScriptableObject
{
    [Header("Перемещение только по Y координате")]
    [Header("-----------------------------------------------------------------------------")]
    public bool isMove_y;
    [Header("Режим уклоняшек")]
    public bool evasionMode;
    [Header("Отображение уровня в 2.5D")]
    public bool mode_2_5D;
    [Range(0.1f, 3f)]
    [Header("Скорость изменения силы броска")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedThrowPower;
    [Range(0.1f, 5f)]
    [Header("Скорость передвижения Hippo")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedHippo;
    [Range(0.1f, 10f)]
    [Header("Скорость перезарядки Hippo")]
    public float speedCooldownHippo;

    [Header("Количество очков для победы")]
    [Header("-----------------------------------------------------------------------------")]
    public int pointsForVictory;

    [Range(0.1f, 10f)]
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

    [Range(0.1f, 3f)]
    [Header("PumaPapa")]
    [Header("Скорость передвижения")]
    [Header("Свойства противников")]
    [Header("-----------------------------------------------------------------------------")]
    public float speedPumaPapa;

    [Range(0.1f, 3f)]
    [Header("PumaMama")]
    public float speedPumaMama;

    [Range(0.1f, 3f)]
    [Header("PumaDaughter")]
    public float speedPumaDaughter;

    [Range(0.1f, 3f)]
    [Header("FoxSon")]
    public float speedFoxSon;

    [Range(0.1f, 3f)]
    [Header("FoxPapa")]
    public float speedFoxPapa;

    [Range(0.1f, 3f)]
    [Header("FoxGrandma")]
    public float speedFoxGrandma;

    [Range(0.1f, 3f)]
    [Header("RaccoonPapa")]
    public float speedRaccoonPapa;

    [Range(0.1f, 3f)]
    [Header("RaccoonSon")]
    public float speedRaccoonSon;

    [Range(0.1f, 3f)]
    [Header("RaccoonGrandpa")]
    public float speedRaccoonGrandpa;

    [Range(0.1f, 10f)]
    [Header("Перезарядка для режима \"Уклоняшки\"")]
    [Header("-----------------------------------------------------------------------------")]
    [Header("PumaPapa")]
    public float speedCooldownPumaPapa;

    [Range(0.1f, 10f)]
    [Header("PumaMama")]
    public float speedCooldownPumaMama;

    [Range(0.1f, 10f)]
    [Header("PumaDaughter")]
    public float speedCooldownPumaDaughter;

    [Range(0.1f, 10f)]
    [Header("FoxSon")]
    public float speedCooldownFoxSon;

    [Range(0.1f, 10f)]
    [Header("FoxPapa")]
    public float speedCooldownFoxPapa;

    [Range(0.1f, 10f)]
    [Header("FoxGrandma")]
    public float speedCooldownFoxGrandma;

    [Range(0.1f, 10f)]
    [Header("RaccoonPapa")]
    public float speedCooldownRaccoonPapa;

    [Range(0.1f, 10f)]
    [Header("RaccoonSon")]
    public float speedCooldownRaccoonSon;

    [Range(0.1f, 10f)]
    [Header("RaccoonGrandpa")]
    public float speedCooldownRaccoonGrandpa;

    [Range(0.1f, 2f)]
    [Header("Скокрость полёта снежков")]
    [Header("-----------------------------------------------------------------------------")]
    [Header("PumaPapa")]
    public float speedSnowballPumaPapa;

    [Range(0.1f, 2f)]
    [Header("PumaMama")]
    public float speedSnowballPumaMama;

    [Range(0.1f, 2f)]
    [Header("PumaDaughter")]
    public float speedSnowballPumaDaughter;

    [Range(0.1f, 2f)]
    [Header("FoxSon")]
    public float speedSnowballFoxSon;

    [Range(0.1f, 2f)]
    [Header("FoxPapa")]
    public float speedSnowballFoxPapa;

    [Range(0.1f, 2f)]
    [Header("FoxGrandma")]
    public float speedSnowballFoxGrandma;

    [Range(0.1f, 2f)]
    [Header("RaccoonPapa")]
    public float speedSnowballRaccoonPapa;

    [Range(0.1f, 2f)]
    [Header("RaccoonSon")]
    public float speedSnowballRaccoonSon;

    [Range(0.1f, 2f)]
    [Header("RaccoonGrandpa")]
    public float speedSnowballRaccoonGrandpa;

    [Header("Вероятность остановки противника")]
    [Header("-----------------------------------------------------------------------------")]
    [Range(0f, 1f)]
    public float chanceStop = 0.1f;
    [Range(0.1f, 10f)]
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
    /// <summary>
    /// Метод позволяющий применить новые настройки, которые были выбранны в инспекторе.
    /// </summary>
    public void ApplySettings()
    {
        // Очки
        pointsPumaPapaComponent.enemyPoints = pointsPumaPapa;
        pointsPumaMamaComponent.enemyPoints = pointsPumaMama;
        pointsPumaDaughterComponent.enemyPoints = pointsPumaDaughter;

        pointsFoxSonComponent.enemyPoints = pointsFoxSon;
        pointsFoxPapaComponent.enemyPoints = pointsFoxPapa;
        pointsFoxGrandmaComponent.enemyPoints = pointsFoxGrandma;

        pointsRaccoonPapaComponent.enemyPoints = pointsRaccoonPapa;
        pointsRaccoonSonComponent.enemyPoints = pointsRaccoonSon;
        pointsRaccoonGrandpaComponent.enemyPoints = pointsRaccoonGrandpa;

        // Противники
        pumaPapaCharacters.speedCharacter = speedPumaPapa;
        pumaPapaCharacters.speedCooldown = speedCooldownPumaPapa;
        pumaPapaCharacters.speedSnowball = speedSnowballPumaPapa;

        pumaMamaCharacters.speedCharacter = speedPumaMama;
        pumaMamaCharacters.speedCooldown = speedCooldownPumaMama;
        pumaMamaCharacters.speedSnowball = speedSnowballPumaMama;

        pumaDaughterCharacters.speedCharacter = speedPumaDaughter;
        pumaDaughterCharacters.speedCooldown = speedCooldownPumaDaughter;
        pumaDaughterCharacters.speedSnowball = speedSnowballPumaDaughter;

        foxSonCharacters.speedCharacter = speedFoxSon;
        foxSonCharacters.speedCooldown = speedCooldownFoxSon;
        foxSonCharacters.speedSnowball = speedSnowballFoxSon;

        foxPapaCharacters.speedCharacter = speedFoxPapa;
        foxPapaCharacters.speedCooldown = speedCooldownFoxPapa;
        foxPapaCharacters.speedSnowball = speedSnowballFoxPapa;

        foxGrandmaCharacters.speedCharacter = speedFoxGrandma;
        foxGrandmaCharacters.speedCooldown = speedCooldownFoxGrandma;
        foxGrandmaCharacters.speedSnowball = speedSnowballFoxGrandma;

        raccoonPapaCharacters.speedCharacter = speedRaccoonPapa;
        raccoonPapaCharacters.speedCooldown = speedCooldownRaccoonPapa;
        raccoonPapaCharacters.speedSnowball = speedSnowballRaccoonPapa;

        raccoonSonCharacters.speedCharacter = speedRaccoonSon;
        raccoonSonCharacters.speedCooldown = speedCooldownRaccoonSon;
        raccoonSonCharacters.speedSnowball = speedSnowballRaccoonSon;

        raccoonGrandpaCharacters.speedCharacter = speedRaccoonGrandpa;
        raccoonGrandpaCharacters.speedCooldown = speedCooldownRaccoonGrandpa;
        raccoonGrandpaCharacters.speedSnowball = speedSnowballRaccoonGrandpa;

        // Остановка противников
        stoppingEnemy.chance = chanceStop;
        stoppingEnemy.time = timeStop;

        // Хиппо
        hippo.speedCharacter = speedHippo;
        hippo.speedCooldown = speedCooldownHippo;
        cooldownHippo.cooldown = speedCooldownHippo;

        // Требуемое количество очков для поебды
        requiredPoints.points = pointsForVictory;


    }
}
