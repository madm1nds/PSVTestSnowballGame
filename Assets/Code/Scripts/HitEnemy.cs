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
        particles.transform.position = other.gameObject.transform.position;
        other.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
        other.GetComponent<CircleCollider2D>().enabled = false;
        particles.SetActive(true);
        scoreSetController.RefreshPoints(points.enemyPoints);
        StartCoroutine(TurnOffParticles());
    }
    IEnumerator TurnOffParticles()
    {        
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        particles.SetActive(false);
    }
}
