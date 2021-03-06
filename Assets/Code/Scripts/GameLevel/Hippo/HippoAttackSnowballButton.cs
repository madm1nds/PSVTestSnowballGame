﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Добавляет логику для кнопки "SnowballButton" в игровом уровне.
/// Запускает атакующую способность и контролирует частоту использования атакующей способности.
/// </summary>
public class HippoAttackSnowballButton : MonoBehaviour
{
    [SerializeField]
    private Button snowballButton;
    [SerializeField]
    private Transform spawnPlace;
    [SerializeField]
    private Slider slider;

    private Vector2 direction;
    private float acceleration;
    private const float torqueSnowball = 45;

    void Start()
    {
        snowballButton.onClick.AddListener(delegate { ThrowSnowball(); });
        acceleration = 1000f;
        direction = new Vector2(0.6f, 1f);
    }
    /// <summary>
    /// Запускает таймер перезарядки атакующей способности. 
    /// После определённого промежутка времени выключает отображение снежка и хвоста снежка
    /// и перемещает снежок на место игрока. После чего снежок снова доступен для использования.
    /// </summary>
    /// <param name="hippoSnowball">GameObject брошенного снежка.</param>
    /// <returns></returns>
    IEnumerator AttackTimer(GameObject hippoSnowball)
    {
        SoundThrow.Run(Vault.instance.audioSourceThrow, Vault.instance.audioClipThrow);

        float timer = 0f;
        Rigidbody2D rb = hippoSnowball.GetComponent<Rigidbody2D>();
        do
        {
            if (PauseButtonController.isPause == false)
            {
                rb.simulated = true;
                timer += 0.018f;
            }
            else
            {
                rb.simulated = false;
            }
            yield return new WaitForSeconds(0.001f);
        }
        while (timer <= 3.5f);

        hippoSnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
        hippoSnowball.transform.GetChild(0).gameObject.SetActive(false);
        hippoSnowball.GetComponent<CircleCollider2D>().enabled = false;
        hippoSnowball.transform.position = spawnPlace.position;

        yield return new WaitForSeconds(0.5f);

        hippoSnowball.SetActive(false);
        hippoSnowball.transform.GetChild(0).gameObject.SetActive(true);
        hippoSnowball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        hippoSnowball.GetComponent<CircleCollider2D>().enabled = true;
        hippoSnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        yield break;
    }
    /// <summary>
    /// Запускает снежок по определённой траектории, с определённой силой, если атакующая способность перезарядилась.
    /// </summary>
    private void ThrowSnowball()
    {
        if (!StatusBarAbilityController.isThrow)
        {
            for (int i = 0; i < Vault.instance.gameObjectHippoSnowballSet.Length; i++)
            {
                if (Vault.instance.gameObjectHippoSnowballSet[i].activeInHierarchy == false)
                {
                    StatusBarAbilityController.instance.InvokeChangeStatus();

                    Vault.instance.transformHippoSnowballSet[i].position = spawnPlace.position;
                    Vault.instance.gameObjectHippoSnowballSet[i].SetActive(true);

                    Vault.instance.rigidbody2DSnowballSet[i].mass = 3f;

                    StartCoroutine(AttackTimer(Vault.instance.gameObjectHippoSnowballSet[i]));

                    if (slider.value < 0.2f)
                    {
                        direction = new Vector2(0.6f, 1f);
                        Vault.instance.rigidbody2DSnowballSet[i].mass -= slider.value;
                    }
                    else if (slider.value < 0.4f)
                    {
                        Vault.instance.rigidbody2DSnowballSet[i].mass -= slider.value + 0.2f;
                        direction = new Vector2(0.65f, 1f);
                    }
                    else if (slider.value < 0.6f)
                    {
                        direction = new Vector2(0.75f, 0.9f);
                        Vault.instance.rigidbody2DSnowballSet[i].mass -= slider.value + 0.4f;
                    }
                    else if (slider.value < 0.8f)
                    {
                        direction = new Vector2(0.8f, 0.85f);
                        Vault.instance.rigidbody2DSnowballSet[i].mass -= slider.value + 0.6f;
                    }
                    else if (slider.value <= 1f)
                    {
                        direction = new Vector2(0.85f, 0.8f);
                        Vault.instance.rigidbody2DSnowballSet[i].mass -= slider.value + 0.8f;
                    }
                    Vault.instance.rigidbody2DSnowballSet[i].AddForce(direction.normalized * acceleration);

                    Vault.instance.rigidbody2DSnowballSet[i].AddTorque(torqueSnowball);
                    break;
                }
            }
        }
    }

}
