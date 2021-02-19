using UnityEngine;
/// <summary>
/// Анимация облаков
/// </summary>
public class CloudsController : MonoBehaviour
{
    [SerializeField]
    private Transform clouds;

    private bool isMoveLeft;
    private const float rightBoundClouds = 110f;
    private const float LeftBoundClouds = -110f;
    private const float speedClouds = 0.005f;

    void FixedUpdate()
    {
        if (isMoveLeft == false && clouds.position.x <= rightBoundClouds)
        {
            clouds.position = new Vector3(clouds.position.x + speedClouds, clouds.position.y, clouds.position.z);
        }
        else
        {
            isMoveLeft = true;
        }
        if (isMoveLeft == true && clouds.position.x >= LeftBoundClouds)
        {
            clouds.position = new Vector3(clouds.position.x - speedClouds, clouds.position.y, clouds.position.z);
        }
        else
        {
            isMoveLeft = false;
        }
    }
}
