using System.Collections;
using UnityEngine;
/// <summary>
/// Класс, который размещает ключевые элементы уровня (Tilemaps) по необходимым координатам.
/// </summary>
public class GroundController : MonoBehaviour
{
    [SerializeField]
    private GameObject groundTop;
    [SerializeField]
    private GameObject groundBottom;
    [SerializeField]
    private GameObject groundLeft;
    [SerializeField]
    private GameObject groundCenter;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject player;
    IEnumerator SetGround()
    {
        yield return new WaitForSeconds(0.001f);
        GroundCoordinates.SetGround(groundBottom, groundTop, groundLeft, groundCenter, player, cam);
        yield break;
    }

    void Start()
    {
        StartCoroutine(SetGround());
    }
}
