using Spine.Unity;
using System.Collections;
using UnityEngine;
/// <summary>
/// Класс управляющий каждым противником.
/// Содержит в себе передвижение противника, запуск атакующей способности и движение снаряда.
/// 
/// characterSettings - содержит настройки передвижения, перезарядки, скорости снарядов для определённого противника
/// stoppingEnemy - содержит настройки остановки для определённого противника
/// spawnPlace - текущее место противника. Место "создания" снежка.
/// currentEnemySkeletonAnimation - управление анимацией противника.
/// currentSpeedEnemy - текущая скорость противника.
/// currentChangeScaleEnemy - изменение размера противника при "отдалении" от камеры.
/// minDistance - минимальая дистанция, которую должен пройти противник, прежде чем он сможет сменить направление движения.
/// speedCorrection - корректирование скокрости противника.
/// speedScaleCorrection - корректирование скорости увеличения протвникапри "отдалении" от камеры.
/// minRandomTimeAttack и maxRandomTimeAttack - создаёт задержку перед атакой, для режима 'Уклоняшки". Без этого, противники атакуют первый раз одновременно.
/// isMoveOut - проверка на то, выходит/входит ли противник за/из пределы поля.
/// isStartedCoroutine - в случае если противник вышел из-за пределов поля.
/// </summary>
public class EnemyController : MonoBehaviour
{
    public Charactrer characterSettings;
    [SerializeField]
    private StoppingEnemy stoppingEnemy;

    private Transform spawnPlace;

    private SkeletonAnimation currentEnemySkeletonAnimation;
    private Transform currentEnemyTransform;

    private float currentSpeedEnemy;
    private float currentChangeScaleEnemy;

    private const float minDistance = 0.6f;
    private const float speedCorrection = 10f;
    private const float speedScaleCorrection = 0.005f;
    private const float minRandomTimeAttack = 1f;
    private const float maxRandomTimeAttack = 2f;
    [HideInInspector] public bool isMoveOut;
    [HideInInspector] public bool isStartedCoroutine = false;

    void Start()
    {
        spawnPlace = transform;
        currentEnemySkeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        currentEnemyTransform = gameObject.transform;
        currentSpeedEnemy = characterSettings.speedCharacter / speedCorrection;
        currentChangeScaleEnemy = characterSettings.speedCharacter * speedScaleCorrection;
    }
    void Update()
    {
        if (isStartedCoroutine == false)
        {
            isStartedCoroutine = true;
            StartCoroutine(MoveEnemy());
            StartCoroutine(Attack());
        }
    }
    /// <summary>
    /// Атака противника
    /// </summary>
    IEnumerator Attack()
    {
        int countEnableEnemySnowball = 0;
        if (Vault.instance.settings.evasionMode == true)
        {
            yield return new WaitForSeconds(Random.Range(minRandomTimeAttack, maxRandomTimeAttack + 1f));
        }
        do
        {
            for (int i = 0; i < Vault.instance.gameObjectEnemySnowballSet.Length; i++)
            {
                if (PauseButtonController.isPause == false && ((Vault.instance.settings.evasionMode == true && !isMoveOut) ||
                    (Vault.instance.settings.evasionMode == false && TimerCooldownNormalModeController.isTimeOut == true && !isMoveOut)))
                {
                    if (Vault.instance.gameObjectEnemySnowballSet[i].activeInHierarchy == false)
                    {
                        TimerCooldownNormalModeController.isTimeOut = false;
                        Vault.instance.gameObjectEnemySnowballSet[i].transform.position = spawnPlace.transform.position;
                        Vault.instance.gameObjectEnemySnowballSet[i].SetActive(true);
                        Vault.instance.transformEnemyTargetSet[i].position = new Vector3(Vault.instance.transformTargetPlayer.position.x, Vault.instance.transformTargetPlayer.position.y, Vault.instance.gameObjectEnemySnowballSet[i].transform.position.z);

                        StartCoroutine(MoveSnowball(Vault.instance.gameObjectEnemySnowballSet[i], Vault.instance.transformEnemyTargetSet[i]));
                        yield return new WaitForSeconds(characterSettings.speedCooldown);

                    }
                    else if (Vault.instance.gameObjectEnemySnowballSet[i].activeInHierarchy == true)
                    {
                        countEnableEnemySnowball++;

                    }
                }
                yield return new WaitForSeconds(0.2f);
            }
            if (countEnableEnemySnowball >= Vault.instance.gameObjectEnemySnowballSet.Length)
            {
                yield return new WaitForSeconds(characterSettings.speedCooldown);
            }
            else
            {
                countEnableEnemySnowball = 0;
                yield return new WaitForSeconds(0.2f);
            }

        } while (true);
    }
    /// <summary>
    /// Движение снежка противника
    /// </summary>
    /// <param name="enemySnowball">Брошеный снежок</param>
    /// <param name="target">Бесконечно отдаляющаяся цель</param>
    /// <returns></returns>
    IEnumerator MoveSnowball(GameObject enemySnowball, Transform target)
    {
        float timer = 0f;

        Transform enemySnowballTransform = enemySnowball.transform;

        do
        {
            if (PauseButtonController.isPause == false)
            {
                timer += 0.015f;
                enemySnowballTransform.position = Vector3.MoveTowards(enemySnowballTransform.position, target.position, characterSettings.speedSnowball / 4);
                enemySnowballTransform.Rotate(0, 0, 15f);
                target.position = Vector3.MoveTowards(target.position, enemySnowballTransform.position, -characterSettings.speedSnowball / 4);
                yield return new WaitForSeconds(0.015f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }

        } while (enemySnowball.GetComponent<CircleCollider2D>().enabled == true && timer <= 3.8f);
        enemySnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
        enemySnowball.transform.GetChild(0).gameObject.SetActive(false);
        enemySnowball.transform.position = spawnPlace.position;

        yield return new WaitForSeconds(0.5f);

        enemySnowball.SetActive(false);
        enemySnowball.transform.GetChild(0).gameObject.SetActive(true);
        enemySnowball.GetComponent<CircleCollider2D>().enabled = true;
        enemySnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        yield break;
    }
    /// <summary>
    /// Передвижение противника
    /// </summary>
    IEnumerator MoveEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        float newEnemyLocation;

        do
        {
            if (PauseButtonController.isPause == false && !isMoveOut)
            {
                newEnemyLocation = Random.Range(ScreenBoundarySeeker.screenBoundary_y_bottom, ScreenBoundarySeeker.screenBoundary_y_top);

                if (System.Math.Abs(System.Math.Abs(currentEnemyTransform.position.y) - System.Math.Abs(newEnemyLocation)) >= minDistance)
                {
                    if (Random.Range(0f, 10f) <= (stoppingEnemy.chance * 10))
                    {
                        currentEnemySkeletonAnimation.AnimationName = "Idle";
                        yield return new WaitForSeconds(stoppingEnemy.time);
                    }
                    else
                    {
                        if (currentEnemyTransform.position.y > newEnemyLocation)
                        {
                            while (currentEnemyTransform.position.y > newEnemyLocation)
                            {
                                if (PauseButtonController.isPause == false)
                                {
                                    currentEnemySkeletonAnimation.AnimationName = "run";
                                    currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x, currentEnemyTransform.position.y - currentSpeedEnemy, currentEnemyTransform.position.z);
                                    if (Vault.instance.settings.mode_2_5D)
                                    {
                                        currentEnemyTransform.localScale = new Vector3(currentEnemyTransform.localScale.x + currentChangeScaleEnemy,
                                            currentEnemyTransform.localScale.y + currentChangeScaleEnemy, currentEnemyTransform.localScale.z);
                                    }
                                }
                                yield return new WaitForSeconds(0.015f);
                            }
                        }
                        else
                        {
                            while (currentEnemyTransform.position.y < newEnemyLocation)
                            {
                                if (PauseButtonController.isPause == false)
                                {
                                    currentEnemySkeletonAnimation.AnimationName = "run";
                                    currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x, currentEnemyTransform.position.y + currentSpeedEnemy, currentEnemyTransform.position.z);

                                    if (Vault.instance.settings.mode_2_5D)
                                    {
                                        currentEnemyTransform.localScale = new Vector3(currentEnemyTransform.localScale.x - currentChangeScaleEnemy,
                                            currentEnemyTransform.localScale.y - currentChangeScaleEnemy, currentEnemyTransform.localScale.z);
                                    }
                                }
                                yield return new WaitForSeconds(0.015f);
                            }
                        }
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        while (true);
    }
}
