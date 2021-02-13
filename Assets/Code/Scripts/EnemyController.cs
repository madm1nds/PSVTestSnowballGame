﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Charactrer characterSettings;
    [SerializeField]
    private StoppingEnemy stoppingEnemy;
    [SerializeField]
    private Transform spawnPlace;
    [SerializeField]
    private Transform targetPlayer;
    [SerializeField]
    private GameObject targetSet;
    [SerializeField]
    private GameObject enemySnowballSet;
    [SerializeField]
    private Settings globalSettings;

    private GameObject[] enemySnowballSetArray;
    private Transform[] targetSetArray;
    private SkeletonAnimation currentEnemySkeletonAnimation;
    private Transform currentEnemyTransform;

    private const float minDistance = 0.6f;
    public bool isMoveOut;
    public bool isStartedCoroutine = false;

    void Start()
    {
        enemySnowballSetArray = new GameObject[enemySnowballSet.transform.childCount];
        targetSetArray = new Transform[targetSet.transform.childCount];
        for (int i = 0; i < enemySnowballSetArray.Length; i++)
        {
            enemySnowballSetArray[i] = enemySnowballSet.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < targetSetArray.Length; i++)
        {
            targetSetArray[i] = targetSet.transform.GetChild(i).transform;
        }
        currentEnemySkeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        currentEnemyTransform = gameObject.transform;
    }
    void Update()
    {
        if (isStartedCoroutine == false)
        {
            //isMoveOut = false;
            isStartedCoroutine = true;
            StartCoroutine(MoveEnemy());
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {

        int isEnable = 0;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        do
        {
            for (int i = 0; i < enemySnowballSetArray.Length; i++)
            {
                if ((globalSettings.evasionMode == true && !isMoveOut) ||
                    (globalSettings.evasionMode == false && EnemyCooldownNormalMode.currentTime >= globalSettings.cooldownSpeedNormalMode && !isMoveOut))
                {
                    if (enemySnowballSetArray[i].activeInHierarchy == false && enemySnowballSetArray[i].tag == "EnemySnowball")
                    {
                        EnemyCooldownNormalMode.currentTime = 0;
                        enemySnowballSetArray[i].transform.position = spawnPlace.transform.position;
                        enemySnowballSetArray[i].SetActive(true);
                        targetSetArray[i].position = new Vector3(targetPlayer.position.x, targetPlayer.position.y, enemySnowballSetArray[i].transform.position.z);

                        StartCoroutine(MoveSnowball(enemySnowballSetArray[i], targetSetArray[i]));
                        yield return new WaitForSeconds(characterSettings.speedCooldown);

                    }
                    else if (enemySnowballSetArray[i].activeInHierarchy == true && enemySnowballSetArray[i].tag == "EnemySnowball")
                    {
                        isEnable++;
                    }
                }
            }
            if (isEnable >= enemySnowballSetArray.Length - 2)
            {
                yield return new WaitForSeconds(characterSettings.speedCooldown);
            }
            else
            {
                isEnable = 0;
                yield return new WaitForSeconds(0.1f);
            }
        } while (true);

    }

    IEnumerator MoveSnowball(GameObject enemySnowball, Transform target)
    {
        float timer = 0f;
        Transform enemySnowballTransform = enemySnowball.transform;
        do
        {
            timer += 0.01f;
            enemySnowballTransform.position = Vector3.MoveTowards(enemySnowballTransform.position, target.position, 0.25f);
            enemySnowballTransform.Rotate(0, 0, 15f);
            target.position = Vector3.MoveTowards(target.position, enemySnowballTransform.position, -0.25f);
            yield return new WaitForSeconds(0.01f);
        } while (enemySnowball.activeInHierarchy == true && timer <= 4);
        enemySnowball.SetActive(false);
        yield break;
    }

    IEnumerator MoveEnemy()
    {
        yield return new WaitForSeconds(0.2f);

        float newLocation = 0;

        do
        {
            if (!isMoveOut)
            {
                currentEnemySkeletonAnimation.AnimationName = "run";
                newLocation = Random.Range(ScreenBoundarySeeker.screenBoundary_y_bottom, ScreenBoundarySeeker.screenBoundary_y_top);

                if (System.Math.Abs(System.Math.Abs(currentEnemyTransform.position.y) - System.Math.Abs(newLocation)) >= minDistance)
                {
                    if (Random.Range(0f, 10f) <= (stoppingEnemy.chance * 10))
                    {
                        currentEnemySkeletonAnimation.AnimationName = "Idle";
                        yield return new WaitForSeconds(stoppingEnemy.time);
                    }
                    else
                    {
                        if (currentEnemyTransform.position.y > newLocation)
                        {
                            while (currentEnemyTransform.position.y > newLocation)
                            {
                                currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x, currentEnemyTransform.position.y - (characterSettings.speedCharacter / 10), currentEnemyTransform.position.z);
                                
                                currentEnemyTransform.localScale = new Vector3(currentEnemyTransform.localScale.x + (characterSettings.speedCharacter * 0.005f),
                                        currentEnemyTransform.localScale.y + (characterSettings.speedCharacter * 0.005f), currentEnemyTransform.localScale.z);
                                yield return new WaitForSeconds(0.01f);
                            }
                        }
                        else
                        {
                            while (currentEnemyTransform.position.y < newLocation)
                            {
                                currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x, currentEnemyTransform.position.y + (characterSettings.speedCharacter / 10), currentEnemyTransform.position.z);
                                
                                currentEnemyTransform.localScale = new Vector3(currentEnemyTransform.localScale.x - (characterSettings.speedCharacter * 0.005f),
                                        currentEnemyTransform.localScale.y - (characterSettings.speedCharacter * 0.005f), currentEnemyTransform.localScale.z);
                                yield return new WaitForSeconds(0.01f);
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
