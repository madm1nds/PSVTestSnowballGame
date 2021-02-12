using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class StartEnemyLocation : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemy;
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
    void Start()
    {
        
        for (int i = 0; i < 3; i++)
        {
            SetLocation(enemy[i]);
        }

    }

    private void SetLocation(Transform enemy)
    {
        isEmpty = true;
        currentLocation = 0;
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
                enemy.position = new Vector3(currentLocation, enemy.position.y, enemy.position.z);
            }
        } while (isEmpty == true);
    }    
}
