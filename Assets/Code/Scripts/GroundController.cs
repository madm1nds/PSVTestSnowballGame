using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Задержка, так как камера не успевает принять нужный вид.
        yield return new WaitForSeconds(0.001f);
        GroundCoordinates.SetGround(groundBottom, groundTop, groundLeft, groundCenter, player, cam);
        yield break;        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetGround());
    }
}
