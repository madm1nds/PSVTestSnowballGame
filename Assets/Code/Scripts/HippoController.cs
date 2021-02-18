using UnityEngine;
using Spine.Unity;
/// <summary>
/// Класс по управлению игроком. 
/// При использовании обычной физики и SimpleInput плагина происходит "не очень красивое" действие.
/// При передвижении персонажа, персонаж может столкнуться с препятствием, однако персонаж "проходит" чуть-чуть 
/// дальше вглубь препятствия, после чего обычная физика выталкивает с небольшим прыжком игрока.
/// Данный класс устраняет данную проблему. Класс использует границы камеры, как препятствие 
/// и не позволяет игроку выйти за пределы поля.
/// А само выталкивание происходит не постоянно, а лишь единожды, когда игрок отталкивается от границы.
/// 
/// player - игрок.
/// speedCharacter - скорректированая скорость персонажа.
/// isMoveNearBarriers - отключает передвижение по определённой оси возле границ экрана
/// playerSkeletonAnimation - управление анимацией игрока.
/// correction - уменьшает слишком большую чувствительность изменения размеров при передвижении.
/// </summary>
public class HippoController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private float speedCharacter;
    private bool isMoveNearBarriers;

    [SerializeField]
    private SkeletonAnimation playerSkeletonAnimation;

    private const float correction = 0.01f;

    void Start()
    {
        speedCharacter = Vault.instance.settings.speedHippo * 0.1f;
    }

    void FixedUpdate()
    {
        InokeCharacterController();
        if (SimpleInput.GetAxis("Horizontal") != 0 || SimpleInput.GetAxis("Vertical") != 0)
        {
            playerSkeletonAnimation.AnimationName = "run";
        }
        else
        {
            playerSkeletonAnimation.AnimationName = "Idle";
        }
    }
    /// <summary>
    /// Метод управления игроком.
    /// </summary>
    void InokeCharacterController()
    {
        if (((Vault.instance.settings.isMove_y == false && SimpleInput.GetAxis("Horizontal") != 0 && SimpleInput.GetAxis("Vertical") != 0)
            || (Vault.instance.settings.isMove_y == true && SimpleInput.GetAxis("Vertical") != 0)) &&
        player.position.y > ScreenBoundarySeeker.screenBoundary_y_bottom &&
        player.position.y < ScreenBoundarySeeker.screenBoundary_y_top &&
        player.position.x > ScreenBoundarySeeker.screenBoundary_x_left &&
        player.position.x < ScreenBoundarySeeker.screenBoundary_x_center)
        {
            MovePlayer();
        }
        else
        {
            if (player.position.y > ScreenBoundarySeeker.screenBoundary_y_bottom)
            {
                if (SimpleInput.GetAxis("Vertical") < 0)
                {
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left)
                    || (SimpleInput.GetAxis("Horizontal") >= 0 && player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center))
                    {
                        MovePlayer();
                    }
                }
                else
                {
                    ReturnToTheField_X_Axis();
                    if (player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left &&
                    player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center)
                    {
                        MovePlayerHorizontally();
                    }
                }
            }
            else
            {
                if (SimpleInput.GetAxis("Vertical") > 0)
                {
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left)
                        || (SimpleInput.GetAxis("Horizontal") >= 0 && player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center))
                    {
                        MovePlayer();
                    }
                }
                else
                {
                    ReturnToTheField_X_Axis();
                    if (player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left &&
                    player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center)
                    {
                        MovePlayerHorizontally();
                    }

                }
            }
            //---------------------------------------------------------------------------------------
            if (player.position.x > ScreenBoundarySeeker.screenBoundary_x_left)
            {
                if (SimpleInput.GetAxis("Horizontal") < 0)
                {
                    if ((SimpleInput.GetAxis("Vertical") <= 0 && player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom)
                         || (SimpleInput.GetAxis("Vertical") >= 0 && player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top))
                    {
                        MovePlayer();
                    }

                }
                else
                {
                    ReturnToTheField_Y_Axis();
                    if (player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom &&
                    player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top)
                    {
                        MovePlayerVertically();
                    }
                }
            }
            else
            {
                if (SimpleInput.GetAxis("Horizontal") > 0)
                {
                    if ((SimpleInput.GetAxis("Vertical") <= 0 && player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom)
                         || (SimpleInput.GetAxis("Vertical") >= 0 && player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top))
                    {
                        MovePlayer();
                    }
                }
                else
                {
                    ReturnToTheField_Y_Axis();
                    if (player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom &&
                    player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top)
                    {
                        MovePlayerVertically();
                    }
                }
            }
        }
        isMoveNearBarriers = false;
    }
    /// <summary>
    /// Передвигает персонажа по любой оси в режиме "Уклоняшки" 
    /// и передвигает по оси "y" для любых других уровней.
    /// </summary>
    void MovePlayer()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            player.position = new Vector3(player.position.x + SimpleInput.GetAxis("Horizontal") * speedCharacter,
                            player.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                            player.position.z);
            if (Vault.instance.settings.mode_2_5D)
            {
                player.localScale = new Vector3(player.localScale.x - (SimpleInput.GetAxis("Vertical") * correction),
                                        player.localScale.y - (SimpleInput.GetAxis("Vertical") * correction), player.localScale.z);
            }
        }
    }
    /// <summary>
    /// Передвигает игрока только по оси "x" в случае, если игрок находится у границ экрана
    /// </summary>
    void MovePlayerHorizontally()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            player.position = new Vector3(player.position.x + SimpleInput.GetAxis("Horizontal") * speedCharacter,
                   player.position.y,
                   player.position.z);
        }
    }
    /// <summary>
    /// Передвигает игрока только по оси "y" в случае, если игрок находится у границ экрана
    /// </summary>
    void MovePlayerVertically()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            player.position = new Vector3(player.position.x,
                       player.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                       player.position.z);
            if (Vault.instance.settings.mode_2_5D)
            {
                player.localScale = new Vector3(player.localScale.x - (SimpleInput.GetAxis("Vertical") * correction),
                            player.localScale.y - (SimpleInput.GetAxis("Vertical") * correction), player.localScale.z);
            }
        }
    }
    /// <summary>
    /// Метод который "выталкивает" персонажа лишь один раз
    /// </summary>
    void ReturnToTheField_Y_Axis()
    {
        if (player.position.y < ScreenBoundarySeeker.screenBoundary_y_bottom && SimpleInput.GetAxis("Vertical") >= 0)
        {
            player.position = new Vector3(player.position.x, player.position.y + correction, player.position.z);
        }

        if (player.position.y > ScreenBoundarySeeker.screenBoundary_y_top && SimpleInput.GetAxis("Vertical") <= 0)
        {
            player.position = new Vector3(player.position.x, player.position.y - correction, player.position.z);
        }
    }
    /// <summary>
    /// Метод который "выталкивает" персонажа лишь один раз
    /// </summary>
    void ReturnToTheField_X_Axis()
    {

        if (player.position.x < ScreenBoundarySeeker.screenBoundary_x_left && SimpleInput.GetAxis("Horizontal") >= 0)
        {
            player.position = new Vector3(player.position.x + correction, player.position.y, player.position.z);
        }

        if (player.position.x > ScreenBoundarySeeker.screenBoundary_x_center && SimpleInput.GetAxis("Horizontal") <= 0)
        {
            player.position = new Vector3(player.position.x - correction, player.position.y, player.position.z);
        }
    }
}
