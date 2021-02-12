using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundarySeeker : MonoBehaviour
{
    private Transform emptinessToSearch;
    [SerializeField]
    private Transform groundTop;
    [SerializeField]
    private Transform groundBottom;
    [SerializeField]
    private Transform groundLeft;
    [SerializeField]
    private Transform groundCenter;

    public static float screenBoundary_y_top;
    public static float screenBoundary_y_bottom;
    public static float screenBoundary_x_left;
    public static float screenBoundary_x_right;
    public static float screenBoundary_x_center;
    // Здесь можно добавить открытые поля для установки других границ.
    void Start()
    {
        emptinessToSearch = gameObject.transform;
        StartCoroutine(FindBoundary());      
    }

    
    IEnumerator FindBoundary()
    {
        yield return new WaitForSeconds(0.01f);
        screenBoundary_x_left = groundLeft.position.x;
        screenBoundary_x_right = System.Math.Abs(groundLeft.position.x);
        screenBoundary_x_center = groundCenter.position.x;


        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y - 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionBottom - 0.1f > groundBottom.position.y);
        screenBoundary_y_bottom = emptinessToSearch.position.y;

        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y + 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionTop + 1.8f < groundTop.position.y);
        screenBoundary_y_top = emptinessToSearch.position.y;

        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y - 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionBottom - 0.1f > groundBottom.position.y);
        screenBoundary_y_bottom = emptinessToSearch.position.y;

        yield break;

    }
}
