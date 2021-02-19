using System.Collections;
using UnityEngine;
/// <summary>
/// Класс, который находит границы экрана в мировых координатах, используя координаты расставленных объектов "Ground"
/// </summary>
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

    /// Чем больше значение, тем меньше доступного пространства в уровне.
    private const float boundary_x_Bottom = 0.1f;
    private const float boundary_x_Top = 1.8f;
    /// Чем больше значение, тем меньше доступного пространства в уровне.
    private const float boundary_y_Left = 0f;
    private const float boundary_y_Right = 0f;
    private const float boundary_y_Center = 0f;

    void Start()
    {
        emptinessToSearch = gameObject.transform;
        StartCoroutine(FindBoundary());
    }

    IEnumerator FindBoundary()
    {
        yield return new WaitForSeconds(0.01f);
        screenBoundary_x_left = groundLeft.position.x + boundary_y_Left;
        screenBoundary_x_right = System.Math.Abs(groundLeft.position.x) - boundary_y_Right;
        screenBoundary_x_center = groundCenter.position.x - boundary_y_Center;

        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y - 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionBottom - boundary_x_Bottom > groundBottom.position.y);
        screenBoundary_y_bottom = emptinessToSearch.position.y;

        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y + 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionTop + boundary_x_Top < groundTop.position.y);
        screenBoundary_y_top = emptinessToSearch.position.y;

        do
        {
            emptinessToSearch.position = new Vector3(emptinessToSearch.position.x, emptinessToSearch.position.y - 0.01f, emptinessToSearch.position.z);
        } while (emptinessToSearch.position.y + GroundCoordinates.correctionBottom - boundary_x_Bottom > groundBottom.position.y);
        screenBoundary_y_bottom = emptinessToSearch.position.y;

        yield break;

    }
}
