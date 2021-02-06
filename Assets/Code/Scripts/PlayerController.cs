using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Charactrer settings;

    [SerializeField]
    private GameObject groundBottom;
    [SerializeField]
    private GameObject groundTop;
    [SerializeField]
    private GameObject groundLeft;
    [SerializeField]
    private GameObject groundRight;

    [SerializeField]
    private Camera cam;

    private float speedCharacter;
    private float speedCooldown;
    private bool isMoveNearBarriers;//чтобы скорость не увеличивалась в десятки раз при скольжении

    SkeletonAnimation skeletonAnimation;


    void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    void Start()
    {
        speedCharacter = settings.speedCharacter * 0.1f;
        speedCooldown = settings.speedCooldown;     
    }


    void FixedUpdate()
    {        
        InokeCharacterController();
        if (SimpleInput.GetAxis("Horizontal") != 0 || SimpleInput.GetAxis("Vertical") != 0)
        {
            skeletonAnimation.AnimationName = "run";
        }
        else
        {
            skeletonAnimation.AnimationName = "Idle";
        }
    }

    void InokeCharacterController()
    {        
        //0.1f и 1.8f - границы края экрана. 
        //Всю эту проверку приходиться делать, так как при использовании обычной физики - персонаж отскакивет от барьеров
        if (SimpleInput.GetAxis("Horizontal") != 0 && SimpleInput.GetAxis("Vertical") != 0 &&
            transform.position.y + GroundCoordinates.correctionBottom - 0.1f > groundBottom.transform.position.y &&
            transform.position.y + GroundCoordinates.correctionTop + 1.8f < groundTop.transform.position.y &&
            transform.position.x > groundLeft.transform.position.x &&
            transform.position.x < groundRight.transform.position.x)
        {            
            MovePlayer();
        }
        else//если мы у стенок
        {   //если позиция игрока выше чем позиция нижнего барьера            
            if (transform.position.y + GroundCoordinates.correctionBottom - 0.1f > groundBottom.transform.position.y)
            {
                // Если мы нажали влево
                if (SimpleInput.GetAxis("Vertical") < 0)
                {
                    //Если мы находимся между боковых барьеров
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && transform.position.x >= groundLeft.transform.position.x)
                    || (SimpleInput.GetAxis("Horizontal") >= 0 && transform.position.x <= groundRight.transform.position.x))
                    {
                        MovePlayer();
                    }
                }
                else //Иначе если мы находимся у стенки
                {
                    ReturnToTheField_X_Axis();

                    if (transform.position.x >= groundLeft.transform.position.x &&
                    transform.position.x <= groundRight.transform.position.x)
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
                    if ((SimpleInput.GetAxis("Horizontal") <= 0 && transform.position.x >= groundLeft.transform.position.x)
                        || (SimpleInput.GetAxis("Horizontal") >= 0 && transform.position.x <= groundRight.transform.position.x))
                    {
                        MovePlayer();
                    }
                }
                else//иначе если мы нажимаем вниз и скользим по стенке
                {
                    ReturnToTheField_X_Axis();

                    if (transform.position.x >= groundLeft.transform.position.x &&
                    transform.position.x <= groundRight.transform.position.x)
                    {
                        MovePlayerHorizontally();
                    }

                }
            }
            //---------------------------------------------------------------------------------------
            if (transform.position.x > groundLeft.transform.position.x)
            {
                if (SimpleInput.GetAxis("Horizontal") < 0) // если мы нажимаем влево и отталкиваемся от правой стенки налево
                {
                    //и и если мы нажимаем вниз и мы находимся выше позиции нижнего барьера
                    //и если мы нажимаем вверх и мы находимся ниже позиции верхнего барьера
                    if ((SimpleInput.GetAxis("Vertical") <= 0 && transform.position.y + GroundCoordinates.correctionBottom - 0.1f >= groundBottom.transform.position.y)
                         || (SimpleInput.GetAxis("Vertical") >= 0 && transform.position.y + GroundCoordinates.correctionTop + 1.8f <= groundTop.transform.position.y))
                    {
                        MovePlayer();
                    }

                }
                else//иначе, если мы нажимаем вправо находясь у барьера
                {
                    ReturnToTheField_Y_Axis();
                    //Если мы находимся выше позиции нижнего барьера или если мы находимся ниже позиции верхнего барьера
                    //то мы скользим по правой стенке
                    if (transform.position.y + GroundCoordinates.correctionBottom - 0.1f >= groundBottom.transform.position.y &&
        transform.position.y + GroundCoordinates.correctionTop + 1.8f <= groundTop.transform.position.y)
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
                    if ((SimpleInput.GetAxis("Vertical") <= 0 && transform.position.y + GroundCoordinates.correctionBottom - 0.1f >= groundBottom.transform.position.y)
                         || (SimpleInput.GetAxis("Vertical") >= 0 && transform.position.y + GroundCoordinates.correctionTop + 1.8f <= groundTop.transform.position.y))
                    {
                        MovePlayer();
                    }
                }
                else
                {
                    ReturnToTheField_Y_Axis();

                    if (transform.position.y + GroundCoordinates.correctionBottom - 0.1f >= groundBottom.transform.position.y &&
                    transform.position.y + GroundCoordinates.correctionTop + 1.8f <= groundTop.transform.position.y)
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
            transform.position = new Vector3(transform.position.x + SimpleInput.GetAxis("Horizontal") * speedCharacter,
                            transform.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                            transform.position.z);
        }
    }
    void MovePlayerHorizontally()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            transform.position = new Vector3(transform.position.x + SimpleInput.GetAxis("Horizontal") * speedCharacter,
                   transform.position.y,
                   transform.position.z);
        }
    }
    void MovePlayerVertically()
    {
        if (!isMoveNearBarriers)
        {
            isMoveNearBarriers = true;
            transform.position = new Vector3(transform.position.x,
                       transform.position.y + SimpleInput.GetAxis("Vertical") * speedCharacter,
                       transform.position.z);
        }
    }
    void ReturnToTheField_Y_Axis()
    {
        // Если нас персонаж вышел за пределы из-за большой скорости ( из-за этого участка когда, физика в игре постоянно "подкидывает" персонажа
        // однако если его вызывать в определённый момент, то тогда этого не видно.)
        //если персонаж вышел ниже барьера и мы нажимаем вверх (чтобы мы не прыгали постоянно в углу)
        if (transform.position.y + GroundCoordinates.correctionBottom - 0.1f < groundBottom.transform.position.y && SimpleInput.GetAxis("Vertical") >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
        //если персонаж вышел выше барьера и мы нажимаем вниз (чтобы мы не прыгали постоянно в углу)
        if (transform.position.y + GroundCoordinates.correctionTop + 1.8f > groundTop.transform.position.y && SimpleInput.GetAxis("Vertical") <= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        }
    }
    void ReturnToTheField_X_Axis()
    {
        //если персонаж вышел левее барьера и мы нажимаем вправо 
        if (transform.position.x < groundLeft.transform.position.x && SimpleInput.GetAxis("Horizontal") >= 0)
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        }
        //если персонаж вышел правее барьера и мы нажимаем влево
        if (transform.position.x > groundRight.transform.position.x && SimpleInput.GetAxis("Horizontal") <= 0)
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        }
    }
}
