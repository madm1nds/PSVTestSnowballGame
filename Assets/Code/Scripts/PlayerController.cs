using System.Collections;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Charactrer settings;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform player;

    private float speedCharacter;
    
    private bool isMoveNearBarriers;//чтобы скорость не увеличивалась в десятки раз при скольжении

    [SerializeField]
    private SkeletonAnimation playerSkeletonAnimation;


    void Start()
    {
        speedCharacter = settings.speedCharacter * 0.1f;
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

    void InokeCharacterController()
    {
        //Всю эту проверку приходиться делать, так как при использовании обычной физики - персонаж отскакивет от барьеров
        if (SimpleInput.GetAxis("Horizontal") != 0 && SimpleInput.GetAxis("Vertical") != 0 &&
        player.position.y > ScreenBoundarySeeker.screenBoundary_y_bottom &&
        player.position.y < ScreenBoundarySeeker.screenBoundary_y_top &&
        player.position.x > ScreenBoundarySeeker.screenBoundary_x_left &&
        player.position.x < ScreenBoundarySeeker.screenBoundary_x_center)
        {
            MovePlayer();
        }
        else//если мы у стенок
        {   //если позиция игрока выше чем позиция нижнего барьера            
            if (player.position.y > ScreenBoundarySeeker.screenBoundary_y_bottom)
            {
                // Если мы нажали влево
                if (SimpleInput.GetAxis("Vertical") < 0)
                {
                    //Если мы находимся между боковых барьеров
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left)
                    || (SimpleInput.GetAxis("Horizontal") >= 0 && player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center))
                    {
                        MovePlayer();
                    }
                }
                else //Иначе если мы находимся у стенки
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
                if (SimpleInput.GetAxis("Vertical") > 0)//Если мы отталкиваемся от нижней стенки вверх
                {
                    //и если мы нажимаем влево и мы находимся слева от правой стенки
                    // или если мы нажимаем вправо и мы находимся справа от левой стенки
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && player.position.x >= ScreenBoundarySeeker.screenBoundary_x_left)
                        || (SimpleInput.GetAxis("Horizontal") >= 0 && player.position.x <= ScreenBoundarySeeker.screenBoundary_x_center))
                    {
                        MovePlayer();
                    }
                }
                else//иначе если мы нажимаем вниз и скользим по стенке
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
                if (SimpleInput.GetAxis("Horizontal") < 0) // если мы нажимаем влево и отталкиваемся от правой стенки налево
                {
                    //и и если мы нажимаем вниз и мы находимся выше позиции нижнего барьера
                    //и если мы нажимаем вверх и мы находимся ниже позиции верхнего барьера
                    if ((SimpleInput.GetAxis("Vertical") <= 0 && player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom)
                         || (SimpleInput.GetAxis("Vertical") >= 0 && player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top))
                    {
                        MovePlayer();
                    }

                }
                else//иначе, если мы нажимаем вправо находясь у барьера
                {
                    ReturnToTheField_Y_Axis();
                    //Если мы находимся выше позиции нижнего барьера или если мы находимся ниже позиции верхнего барьера
                    //то мы скользим по правой стенке
                    if (player.position.y >= ScreenBoundarySeeker.screenBoundary_y_bottom &&
                    player.position.y <= ScreenBoundarySeeker.screenBoundary_y_top)
                    {
                        MovePlayerVertically();
                    }
                }
            }
            else
            {
                if (SimpleInput.GetAxis("Horizontal") > 0)//если мы отталкиваемся от левой стенки направо
                {
                    //и и если мы нажимаем вниз и мы находимся выше позиции нижнего барьера
                    //и если мы нажимаем вверх и мы находимся ниже позиции верхнего барьера
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
    void MovePlayer()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            player.position = new Vector3(player.position.x + SimpleInput.GetAxis("Horizontal") * speedCharacter,
                            player.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                            player.position.z);
            player.localScale = new Vector3(player.localScale.x - (SimpleInput.GetAxis("Vertical") * 0.01f),
                                        player.localScale.y - (SimpleInput.GetAxis("Vertical") * 0.01f), player.localScale.z);
        }
    }
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
    void MovePlayerVertically()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            player.position = new Vector3(player.position.x,
                       player.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                       player.position.z);
            player.localScale = new Vector3(player.localScale.x - (SimpleInput.GetAxis("Vertical") * 0.01f),
                            player.localScale.y - (SimpleInput.GetAxis("Vertical") * 0.01f), player.localScale.z);
        }
    }
    void ReturnToTheField_Y_Axis()
    {
        // Если нас персонаж вышел за пределы из-за большой скорости ( из-за этого участка когда, физика в игре постоянно "подкидывает" персонажа
        // однако если его вызывать в определённый момент, то тогда этого не видно.)
        //если персонаж вышел ниже барьера и мы нажимаем вверх (чтобы мы не прыгали постоянно в углу)
        if (player.position.y < ScreenBoundarySeeker.screenBoundary_y_bottom && SimpleInput.GetAxis("Vertical") >= 0)
        {
            player.position = new Vector3(player.position.x, player.position.y + 0.01f, player.position.z);
        }
        //если персонаж вышел выше барьера и мы нажимаем вниз (чтобы мы не прыгали постоянно в углу)
        if (player.position.y > ScreenBoundarySeeker.screenBoundary_y_top && SimpleInput.GetAxis("Vertical") <= 0)
        {
            player.position = new Vector3(player.position.x, player.position.y - 0.01f, player.position.z);
        }
    }
    void ReturnToTheField_X_Axis()
    {
        //если персонаж вышел левее барьера и мы нажимаем вправо 
        if (player.position.x < ScreenBoundarySeeker.screenBoundary_x_left && SimpleInput.GetAxis("Horizontal") >= 0)
        {
            player.position = new Vector3(player.position.x + 0.01f, player.position.y, player.position.z);
        }
        //если персонаж вышел правее барьера и мы нажимаем влево
        if (player.position.x > ScreenBoundarySeeker.screenBoundary_x_center && SimpleInput.GetAxis("Horizontal") <= 0)
        {
            player.position = new Vector3(player.position.x - 0.01f, player.position.y, player.position.z);
        }
    }
}
