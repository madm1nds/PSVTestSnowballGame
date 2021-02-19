using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Определяет страртовую позицию при загрузке уровня.
/// </summary>
public class EnemyStartLocation : MonoBehaviour
{
    public static EnemyStartLocation instance;
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
    private Transform[][] enemyLevels;
    public static Dictionary<int, bool> locations = new Dictionary<int, bool>
        {
            { position_1, false },

            { position_2, false },

            { position_3, false },

            { position_4, false }
        };
    private int currentLocation;
    private bool isEmpty;
    private const float defaultScale = 0.6f;
    private const int countLevels = 5;
    private const int position_1 = 3;
    private const int position_2 = 5;
    private const int position_3 = 7;
    private const int position_4 = 9;

    // Start is called before the first frame update
    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<EnemyStartLocation>();
        }
        enemyLevels = new Transform[countLevels][];
        enemyLevels[0] = enemyLevel_1;
        enemyLevels[1] = enemyLevel_2;
        enemyLevels[2] = enemyLevel_3;
        enemyLevels[3] = enemyLevel_4;
        enemyLevels[4] = enemyLevel_5;
        StartCoroutine(ResetLocation(1));
    }
    /// <summary>
    /// Запуск определения страртовой позиции при загрузке уровня.
    /// </summary>
    /// <param name="numberLevel">Номер загружаемого уровня</param>
    public IEnumerator ResetLocation(int numberLevel)
    {
        TimerCooldownNormalModeController.isTimeOut = false;
        TimerCooldownNormalModeController.time = 0;
        for (int i = 0; i < Vault.instance.gameObjectEnemySnowballSet.Length; i++)
        {
            if (Vault.instance.gameObjectEnemySnowballSet[i].activeInHierarchy == true)
            {
                Vault.instance.gameObjectEnemySnowballSet[i].SetActive(false);
            }
        }

        for (int i = 0; i < Vault.instance.transformGameObjectEnemies.Length; i++)
        {
            Vault.instance.transformGameObjectEnemies[i].gameObject.SetActive(false);
            Vault.instance.transformGameObjectEnemies[i].localScale = new Vector3(defaultScale, defaultScale, defaultScale);
            Vault.instance.enemyControllers[i].isMoveOut = false;
            Vault.instance.enemyControllers[i].isStartedCoroutine = false;

            if (Vault.instance.transformGameObjectEnemies[i].transform.rotation.y == 1)
            {
                Vault.instance.transformGameObjectEnemies[i].transform.Rotate(0, -180, 0);
            }
            if (Vault.instance.transformGameObjectEnemies[i].transform.rotation.y == -1)
            {
                Vault.instance.transformGameObjectEnemies[i].transform.Rotate(0, 180, 0);
            }
        }

        locations.Clear();
        locations.Add(position_1, false);
        locations.Add(position_2, false);
        locations.Add(position_3, false);
        locations.Add(position_4, false);

        for (int i = 0; i < enemyLevels[numberLevel].Length; i++)
        {
            enemyLevels[numberLevel][i].gameObject.SetActive(true);
            SetStartLocation(enemyLevels[numberLevel][i]);
        }

        Vault.instance.gameObjectEnemies.SetActive(true);
        yield break;
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
