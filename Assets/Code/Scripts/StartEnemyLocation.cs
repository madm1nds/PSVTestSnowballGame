using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class StartEnemyLocation : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemyLevel_1;
    [SerializeField]
    private Transform[] enemyLevel_2;
    [SerializeField]
    private Transform[] enemyLevel_3;
    [SerializeField]
    private Transform[] enemyLevel_4;
    [SerializeField]
    private Transform[] enemyLevel_5;
    public static Dictionary<int, bool> locations = new Dictionary<int, bool>
        {
            { 3, false },

            { 5, false },

            { 7, false },

            { 9, false }
        };
    int currentLocation;
    bool isEmpty;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < enemyLevel_1[0].parent.childCount; i++)
        {
            enemyLevel_1[0].parent.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyLevel_1.Length; i++)
        {
            enemyLevel_1[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < 3; i++)
        {
            SetStartLocation(enemyLevel_1[i]);
        }

    }

    private void SetStartLocation(Transform enemy)
    {
        isEmpty = true;
        currentLocation = 0;
        float centerPosition;
        do
        {            
            currentLocation = Random.Range(3, 10);

            foreach (var coordinates in locations)
            {
                if (coordinates.Key == currentLocation && coordinates.Value == false)
                {
                    isEmpty = false;
                }
            }
            if (isEmpty == false)
            {
                locations.Remove(currentLocation);
                locations.Add(currentLocation, true);

                centerPosition = ScreenBoundarySeeker.screenBoundary_y_bottom + 
                    ((System.Math.Abs(ScreenBoundarySeeker.screenBoundary_y_bottom) + System.Math.Abs(ScreenBoundarySeeker.screenBoundary_y_top)) / 2);

                enemy.position = new Vector3(currentLocation, centerPosition, enemy.position.z);
            }
        } while (isEmpty == true);
    }
}
