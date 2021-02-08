using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
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
    [SerializeField]
    private GameObject player;
    IEnumerator SetGround()
    {
        // Задержка, так как камера не успевает принять нужный вид.
        yield return new WaitForSeconds(0.001f);
        GroundCoordinates.SetGround(groundBottom, groundTop, groundLeft, groundRight, player, cam);
        StopCoroutine(SetGround());
        yield return new WaitForSeconds(1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetGround());
    }
}
