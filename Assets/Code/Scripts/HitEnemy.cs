using Spine.Unity;
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

    private SkeletonAnimation enemySkeletonAnimation;
    void Start()
    {
        enemySkeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snowball")
        {
            particles.transform.position = other.gameObject.transform.position;
            StartCoroutine(WaitOffTrail(other.gameObject));
            particles.SetActive(true);
            StartCoroutine(TurnOffParticles());
            // Если противник не убегает за экран и не приходит новый противник
            if (GetComponent<EnemyController>().isMoveOut == false)
            {
                scoreSetController.RefreshPoints(points.enemyPoints);
                StartCoroutine(MoveEnemy(transform, GetComponent<EnemyController>()));
            }
            else
            {
                scoreSetController.RefreshPoints(1);
            }
        }
    }
    IEnumerator WaitOffTrail(GameObject other)
    {
        other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        other.GetComponent<CircleCollider2D>().enabled = false;
        other.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,0);
        yield return new WaitForSeconds(1f);
        other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        other.GetComponent<CircleCollider2D>().enabled = true;
        other.gameObject.SetActive(false);        
        other.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        yield break;
    }
    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        particles.SetActive(false);
        yield break;
    }
    IEnumerator MoveEnemy(Transform currentEnemyTransform, EnemyController enemyController)
    {
        int positionInHierarchy = 0;
        Transform newEnemyTransform;
        SkeletonAnimation newEnemySkeletonAnimation;
        EnemyController newEnemyController;
        // Уход противника за экран и выключение противника
        enemyController.isMoveOut = true;
        currentEnemyTransform.Rotate(0, 180, 0);
        StartEnemyLocation.locations.Remove((int)currentEnemyTransform.position.x);
        StartEnemyLocation.locations.Add((int)currentEnemyTransform.position.x, false);
        while (currentEnemyTransform.position.x < ScreenBoundarySeeker.screenBoundary_x_right + 2)
        {
            if (PauseButtonController.isPause == false)
            {
                enemySkeletonAnimation.AnimationName = "run";
                currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x + (enemyController.characterSettings.speedCharacter / 10), currentEnemyTransform.position.y, currentEnemyTransform.position.z);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        }
        currentEnemyTransform.Rotate(0, -180, 0);
        // Включить нового противника и переместить на новую точку 
        do
        {
            if (PauseButtonController.isPause == false)
            {
                positionInHierarchy = Random.Range(0, currentEnemyTransform.parent.childCount);

                if (currentEnemyTransform.parent.GetChild(positionInHierarchy).gameObject.activeInHierarchy == false)
                {
                    newEnemyTransform = currentEnemyTransform.parent.GetChild(positionInHierarchy);
                    newEnemySkeletonAnimation = currentEnemyTransform.parent.GetChild(positionInHierarchy).GetComponent<SkeletonAnimation>();
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        } while (true);

        newEnemyTransform.localScale = currentEnemyTransform.localScale;
        newEnemyTransform.position = currentEnemyTransform.position;
        newEnemyController = newEnemyTransform.GetComponent<EnemyController>();
        newEnemyController.isMoveOut = true;
        newEnemyController.isStartedCoroutine = false;
        newEnemyTransform.gameObject.SetActive(true);

        Vector3 newPosition = EnemyNewLocation.GetNewLocation(newEnemyTransform);
        while (newEnemyTransform.position.x > newPosition.x)
        {
            if (PauseButtonController.isPause == false)
            {
                newEnemySkeletonAnimation.AnimationName = "run";
                newEnemyTransform.position = new Vector3(newEnemyTransform.position.x - (newEnemyController.characterSettings.speedCharacter / 10), newEnemyTransform.position.y, newEnemyTransform.position.z);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        newEnemyTransform.position = new Vector3(newPosition.x, newEnemyTransform.position.y, newEnemyTransform.position.z);
        newEnemySkeletonAnimation.AnimationName = "Idle";
        newEnemyController.isMoveOut = false;
        currentEnemyTransform.gameObject.SetActive(false);
        yield break;
    }
}
