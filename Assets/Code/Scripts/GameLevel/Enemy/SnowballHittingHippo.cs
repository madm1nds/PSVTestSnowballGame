using System.Collections;
using UnityEngine;
/// <summary>
/// Момент столкновения снежка противника с коллайдером игрока.
/// </summary>
public class SnowballHittingHippo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D enemySnowball)
    {
        if (enemySnowball.CompareTag("EnemySnowball"))
        {
            SoundThrow.Run(Vault.instance.audioSourceHit, Vault.instance.audioClipHit);
            enemySnowball.GetComponent<CircleCollider2D>().enabled = false;
            enemySnowball.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
            for (int i = 0; i < Vault.instance.gameObjectsEnemySetParticleSystems.Length; i++)
            {
                if (Vault.instance.gameObjectsEnemySetParticleSystems[i].activeInHierarchy == false)
                {
                    Vault.instance.gameObjectsEnemySetParticleSystems[i].transform.position = enemySnowball.gameObject.transform.position;
                    Vault.instance.gameObjectsEnemySetParticleSystems[i].SetActive(true);
                    StartCoroutine(TurnOffParticles(Vault.instance.gameObjectsEnemySetParticleSystems[i]));

                    InitSettings.healthPoints--;
                    switch (InitSettings.healthPoints)
                    {
                        case 2:
                            Vault.instance.imageUIHearts[0].sprite = Vault.instance.spriteBrokenHeart;
                            Vault.instance.spriteRendererStarRight.sprite = Vault.instance.spriteStarRightOff;
                            break;
                        case 1:
                            Vault.instance.imageUIHearts[1].sprite = Vault.instance.spriteBrokenHeart;
                            Vault.instance.spriteRendererStarCenter.sprite = Vault.instance.spriteStarCenterOff;
                            break;
                        case 0:
                            Vault.instance.imageUIHearts[2].sprite = Vault.instance.spriteBrokenHeart;
                            PauseButtonController.instance.ClickOnPause();
                            Vault.instance.gameObjectVictoryBoard.SetActive(true);
                            Vault.instance.gameObjectVictoryBoardRunLevel.SetActive(false);
                            Vault.instance.gameObjectStarLeft.SetActive(true);
                            Vault.instance.gameObjectStarCenter.SetActive(true);
                            Vault.instance.gameObjectStarRight.SetActive(true);
                            Vault.instance.spriteRendererStarLeft.sprite = Vault.instance.spriteStarLeftOff;
                            Vault.instance.spriteRendererTextVictoryBoard.sprite = LanguageController.ChangeLanguage(SpriteName.Fiasco);//Vault.instance.spriteLivesEndedRus;
                            StartCoroutine(SoundWinFail.Run(Vault.instance.audioClipFail));
                            break;
                    }
                    break;
                }
            }
        }
    }
    /// <summary>
    /// Выключает частицы после окончания времени взрыва
    /// </summary>
    IEnumerator TurnOffParticles(GameObject particles)
    {
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.duration);
        particles.SetActive(false);
        yield break;
    }
}
