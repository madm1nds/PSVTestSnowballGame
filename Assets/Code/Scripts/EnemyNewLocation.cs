using UnityEngine;
/// <summary>
/// Статический класс определения новой позиции для нового противника
/// </summary>
public static class EnemyNewLocation
{
    static int currentLocation;
    static bool isEmpty;
    public static Vector3 GetNewLocation(Transform enemy)
    {
        isEmpty = true;
        currentLocation = 0;
        do
        {
            currentLocation = Random.Range(3, 10);

            foreach (var coordinates in EnemyStartLocation.locations)
            {
                if (coordinates.Key == currentLocation && coordinates.Value == false)
                {
                    isEmpty = false;
                }
            }
            if (isEmpty == false)
            {
                EnemyStartLocation.locations.Remove(currentLocation);
                EnemyStartLocation.locations.Add(currentLocation, true);
                return new Vector3(currentLocation, enemy.position.y, enemy.position.z);
            }
        } while (isEmpty == true);
        return enemy.transform.position;
    }
}
