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
    public static int startPosition = 0;
    public static SystemLanguage currentLanguage; 
    [Header("Глобальные настройки")]
    public Settings settings;
    public bool isMusic;
    public bool isSounds;
    public bool isEffects;

    [Header("AudioSources")]
    public AudioSource audioSourceGameMusic;
    public AudioSource audioSourcePressButton;
    public AudioSource audioSourceWinFail;
    public AudioSource[] audioSourceThrow;
    public AudioSource[] audioSourceHit;

    [Header("AudioClips")]
    public AudioClip audioClipWin;
    public AudioClip audioClipFail;
    public AudioClip[] audioClipThrow;
    public AudioClip[] audioClipHit;

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

    [Header("Localisation")]
    public Sprite spriteEvasionModeRus;
    public Sprite spriteExcellentRus;
    public Sprite spriteFiascoRus;
    public Sprite spritePauseRus;
    public Sprite spriteThrowPowerRus;
    [Header("------------------------------------------------------------------------------------")]
    public Sprite spriteEvasionModeEng;
    public Sprite spriteExcellentEng;
    public Sprite spriteFiascoEng;
    public Sprite spritePauseEng;
    public Sprite spriteThrowPowerEng;
    [Header("------------------------------------------------------------------------------------")]
    public Sprite spriteEvasionModeJap;
    public Sprite spriteExcellentJap;
    public Sprite spriteFiascoJap;
    public Sprite spritePauseJap;
    public Sprite spriteThrowPowerJap;

    [Header("Sprites")]
    public Sprite[] spriteSnowball;

    public Sprite spriteGoodHeart;
    public Sprite spriteBrokenHeart;

    public Sprite spriteStarLeftOff;
    public Sprite spriteStarCenterOff;
    public Sprite spriteStarRightOff;
    public Sprite spriteStarLeftOn;
    public Sprite spriteStarCenterOn;
    public Sprite spriteStarRightOn;

    public Sprite spriteThumbJoystickXY;
    public Sprite spriteThumbJoystickY;

    public Sprite spriteMusicOn;
    public Sprite spriteMusicOff;
    public Sprite spriteSoundsOn;
    public Sprite spriteSoundsOff;
    public Sprite spriteEffectsOn;
    public Sprite spriteEffectsOff;

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
    public Button buttonUIMusic;
    public Button buttonUISounds;
    public Button buttonUIEffects;
    [Header("Buttons Language")]
    public Button buttonUIRussian;
    public Button buttonUIEnglish;
    public Button buttonUIJapanese;

    [Header("Images")]
    public Image imageUISnowballButton;
    public Image imageUIThumbJoystick;
    public Image[] imageUIHearts;
    public Image[] imageGameLevelUI;
    [Header("------------------------------------------------------------------------------------")]
    public Image[] imageMainMenuVaultButton;
    public Image[] imageItemsMenu;
    public Image[] imageStartGameMenu;
    public Image[] imageSettingsMenu;
    public Image imageUIMusic;
    public Image imageUISounds;
    public Image imageUIEffects;
    public Image imageUIEvasionButton;
    public Image imageUIThrowPower;

    [Header("ParticleSystem")]
    public ParticleSystem particleSystemHippoSnowball;
    [SerializeField] private ParticleSystem psVictoryBoard;
    public ParticleSystem.MainModule particleSystemVictoryBoard;
    [SerializeField] private ParticleSystem psSnow;
    public ParticleSystem.MainModule particleSystemSnow;

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
        particleSystemSnow = psSnow.GetComponent<ParticleSystem>().main;
    }
}
/// <summary>
/// Перечисление названий спрайтов, язык которых будет меняться.
/// </summary>
public enum SpriteName
{
    EvasionMode,
    Excellent,
    Fiasco,
    Pause,
    ThrowPower
}