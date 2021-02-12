using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private EnemyPoints points;
    [SerializeField]
    private ScoreSetController scoreSetController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snowball")
        {
            particles.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            particles.SetActive(true);
            scoreSetController.RefreshPoints(points.enemyPoints);
            StartCoroutine(TurnOffParticles());
        }
    }
    IEnumerator TurnOffParticles()
    {        
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        particles.SetActive(false);
        yield break;
    }
}
