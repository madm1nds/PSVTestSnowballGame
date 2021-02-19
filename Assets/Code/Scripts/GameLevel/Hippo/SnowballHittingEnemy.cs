using Spine.Unity;
using System.Collections;
using UnityEngine;
/// <summary>
/// Момент столкновения снежка игрока с коллайдером противника.
/// </summary>
public class SnowballHittingEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyPoints points;

    private SkeletonAnimation enemySkeletonAnimation;
    private const float gravityValue = 0f;
    private const int bonusPoint = 1;
    private const int moreRightBorder = 2;
    private const float correctionSpeedEnemy = 10f;
    void Start()
    {
        enemySkeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void OnTriggerEnter2D(Collider2D hippoSnowball)
    {
        if (hippoSnowball.CompareTag("Snowball"))
        {
            SoundThrow.Run(Vault.instance.audioSourceHit, Vault.instance.audioClipHit);
            Vault.instance.particleSystemHippoSnowball.transform.position = hippoSnowball.gameObject.transform.position;
            hippoSnowball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            hippoSnowball.GetComponent<CircleCollider2D>().enabled = false;
            hippoSnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
            Vault.instance.particleSystemHippoSnowball.gameObject.SetActive(true);
            StartCoroutine(TurnOffParticles());

            if (ScoreSetController.scorePlayer < Vault.instance.settings.pointsForVictory)
            {
                if (GetComponent<EnemyController>().isMoveOut == false)
                {
                    ScoreSetController.instance.RefreshPoints(points.enemyPoints);
                    StartCoroutine(MoveEnemy(transform, GetComponent<EnemyController>()));
                }
                else
                {
                    ScoreSetController.instance.RefreshPoints(bonusPoint);
                }
            }
            if (ScoreSetController.scorePlayer >= Vault.instance.settings.pointsForVictory)
            {
                PauseButtonController.instance.ClickOnPause();
                Vault.instance.gameObjectVictoryBoard.SetActive(true);
                Vault.instance.gameObjectVictoryBoardRunLevel.SetActive(false);
                Vault.instance.gameObjectStarLeft.SetActive(true);
                Vault.instance.gameObjectStarCenter.SetActive(true);
                Vault.instance.gameObjectStarRight.SetActive(true);
                Vault.instance.spriteRendererTextVictoryBoard.sprite = LanguageController.ChangeLanguage(SpriteName.Excellent);//Vault.instance.spriteExcellentRus;

                Vault.instance.particleSystemVictoryBoard.gravityModifier = gravityValue;

                StartCoroutine(SoundWinFail.Run(Vault.instance.audioClipWin));
            }
        }
    }

    /// <summary>
    /// выключает частицы после окончания времени взрыва
    /// </summary>
    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(Vault.instance.particleSystemHippoSnowball.main.duration);
        Vault.instance.particleSystemHippoSnowball.gameObject.SetActive(false);
        yield break;
    }
    /// <summary>
    /// Передвижение проигравшего противника за пределы поля и передвижение нового противника при вхождении на поле
    /// </summary>
    /// <param name="currentEnemyTransform">Transform противника в которого попали снежком</param>
    /// <param name="enemyController">EnemyController противника в которого попали снежком</param>
    /// <returns></returns>
    IEnumerator MoveEnemy(Transform currentEnemyTransform, EnemyController enemyController)
    {
        int positionInHierarchy;
        Transform newEnemyTransform;
        SkeletonAnimation newEnemySkeletonAnimation;
        EnemyController newEnemyController;
        //--------------------------
        enemyController.isMoveOut = true;
        currentEnemyTransform.Rotate(0, 180, 0);
        EnemyStartLocation.locations.Remove((int)currentEnemyTransform.position.x);
        EnemyStartLocation.locations.Add((int)currentEnemyTransform.position.x, false);
        while (currentEnemyTransform.position.x < ScreenBoundarySeeker.screenBoundary_x_right + moreRightBorder)
        {
            if (PauseButtonController.isPause == false)
            {
                enemySkeletonAnimation.AnimationName = "run";
                currentEnemyTransform.position = new Vector3(currentEnemyTransform.position.x + (enemyController.characterSettings.speedCharacter / correctionSpeedEnemy),
                                currentEnemyTransform.position.y, currentEnemyTransform.position.z);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        currentEnemyTransform.Rotate(0, -180, 0);
        //--------------------------
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
                newEnemyTransform.position = new Vector3(newEnemyTransform.position.x - (newEnemyController.characterSettings.speedCharacter / correctionSpeedEnemy),
                                newEnemyTransform.position.y, newEnemyTransform.position.z);
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
