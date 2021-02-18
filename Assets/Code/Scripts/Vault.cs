using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс представляет собой хранилище игровых объектов (GameObject) и их компоненты.
/// Используется для удобства и оптимизации игры. 
/// Вместо постоянного объявления полей в скриптах, объявление производится лишь единожды.
/// Также позволяет избежать, постоянного "перетаскивания" объектов из иерархии в инспетор.
/// </summary>
public class Vault : MonoBehaviour
{
    public static Vault instance;
    [Header("Глобальные настройки")]
    public Settings settings;

    [Header("Игровые объекты на сцене")]
    public GameObject gameObjectGameLevelUI;
    public GameObject gameObjectMainMenuUI;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectMainMenuVaultButton;
    public GameObject gameObjectItemsMenu;
    public GameObject gameObjectStartGameMenu;
    public GameObject gameObjectSettingsMenu;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject[] gameObjectsItemsMenu;
    public GameObject[] gameObjectsStartGameMenu;
    public GameObject[] gameObjectsSettingsMenu;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectHippo;
    public GameObject gameObjectEnemies;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject[] gameObjectHippoSnowballSet;
    public GameObject[] gameObjectEnemySnowballSet;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectVictoryBoard;
    public GameObject gameObjectVictoryBoardRunLevel;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectStatusBarAbility;
    public GameObject gameObjectStatusBarAbilityMask;
    public GameObject gameObjectHippoReadySnowball;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject[] gameObjectsEnemySetParticleSystems;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectStarLeft;
    public GameObject gameObjectStarCenter;
    public GameObject gameObjectStarRight;
    [Header("------------------------------------------------------------------------------------")]
    public GameObject gameObjectMainMenu2D;
    public SpriteRenderer spriteRendererMainMenuBackground;
    public GameObject[] gameObjectGround;

    [Header("UI игровые объекты на сцене")]
    public GameObject[] gameObjectMainMenu;
    public GameObject[] gameObjectContentMainMenu;

    [Header("Transform объектов")]
    public Transform[] transformEnemyTargetSet;
    public Transform[] transformHippoSnowballSet;
    public Transform transformTargetPlayer;
    public Transform transformStatusBarAbilityMask;
    [Header("------------------------------------------------------------------------------------")]
    public Transform[] transformGameObjectEnemies;

    [Header("Sprites")]
    public Sprite[] spriteSnowball;

    public Sprite spriteGoodHeart;
    public Sprite spriteBrokenHeart;

    public Sprite spriteExcellentRus;
    public Sprite spriteLivesEndedRus;
    public Sprite spritePauseRus;
    public Sprite spriteTrowPowerRus;

    public Sprite spriteStarLeftOff;
    public Sprite spriteStarCenterOff;
    public Sprite spriteStarRightOff;
    public Sprite spriteStarLeftOn;
    public Sprite spriteStarCenterOn;
    public Sprite spriteStarRightOn;

    [Header("SpriteRenderers")]
    public SpriteRenderer spriteRendererTextVictoryBoard;
    public SpriteRenderer spriteRendererStarLeft;
    public SpriteRenderer spriteRendererStarCenter;
    public SpriteRenderer spriteRendererStarRight;

    [Header("Slider")]
    public Slider sliderUISlider;

    [Header("Buttons")]
    public Button[] buttonUIMainMenu;
    public Button buttonUIBackButton;
    public Button[] buttonUIItemsMenu;

    [Header("Images")] 
    public Image[] imageUIHearts;
    public Image[] imageGameLevelUI;
    [Header("------------------------------------------------------------------------------------")]
    public Image[] imageMainMenuVaultButton;
    public Image[] imageItemsMenu;
    public Image[] imageStartGameMenu;
    public Image[] imageSettingsMenu;

    [Header("ParticleSystem")]
    public ParticleSystem particleSystemHippoSnowball;
    [SerializeField] private ParticleSystem psVictoryBoard;
    public ParticleSystem.MainModule particleSystemVictoryBoard;

    [Header("Rigidbody2D")]
    public Rigidbody2D[] rigidbody2DSnowballSet;

    [Header("SkeletonAnimation")]
    public Spine.Unity.SkeletonAnimation[] skeletonAnimationEnemies;

    [Header("Joystick")]
    public SimpleInputNamespace.Joystick joystickUIJoystick;

    [Header("EnemyController")]
    public EnemyController[] enemyControllers;

    private void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<Vault>();
        }
        particleSystemVictoryBoard = psVictoryBoard.GetComponent<ParticleSystem>().main;
    }
}
