using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitHippo : MonoBehaviour
{
    [SerializeField]
    private GameObject particlesSet;
    [SerializeField]
    private GameObject heartsSet;
    [SerializeField]
    private Sprite goodHeart;
    [SerializeField]
    private Sprite brokenHeart;
    /// <summary>
    /// Поля для оптимизации.
    /// </summary>
    private GameObject[] particlesSetArray;
    private Image[] heartsSetArray;
    /// <summary>
    /// Потребляет немного больше памяти, но оптмизация становится лучше.
    /// </summary>
    void Start()
    {
        particlesSetArray = new GameObject[particlesSet.transform.childCount];
        heartsSetArray = new Image[heartsSet.transform.childCount];
        for (int i = 0; i < particlesSetArray.Length; i++)
        {
            particlesSetArray[i] = particlesSet.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < heartsSetArray.Length; i++)
        {
            heartsSetArray[i] = heartsSet.transform.GetChild(i).GetComponent<Image>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemySnowball")
        {
            StartCoroutine(WaitOffTrail(other.gameObject));
            //other.gameObject.SetActive(false);
            for (int i = 0; i < particlesSetArray.Length; i++)
            {
                if (particlesSetArray[i].activeInHierarchy == false)
                {
                    particlesSetArray[i].transform.position = other.gameObject.transform.position;
                    particlesSetArray[i].SetActive(true);
                    StartCoroutine(TurnOffParticles(particlesSetArray[i]));

                    InitSettings.healthPoints--;
                    switch (InitSettings.healthPoints)
                    {
                        case 2: heartsSetArray[0].sprite = brokenHeart; break;
                        case 1: heartsSetArray[1].sprite = brokenHeart; break;
                        case 0: heartsSetArray[2].sprite = brokenHeart; break;
                    }
                    break;
                }
            }
        }
    }
    IEnumerator WaitOffTrail(GameObject other)
    {
        other.GetComponent<CircleCollider2D>().enabled = false;
        other.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
        yield return new WaitForSeconds(1f);
        other.GetComponent<CircleCollider2D>().enabled = true;
        other.gameObject.SetActive(false);
        other.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        yield break;
    }
    IEnumerator TurnOffParticles(GameObject particles)
    {
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        particles.SetActive(false);
        yield break;
    }
}
