using UnityEngine;
/// <summary>
/// Класс, который контролирует нажатие на кнопки в "VictoryBoard".
/// Так как данный объект является 2D (для синхронизации с частицами) а не UI, используется Raycast вместо компонента <Button>().
/// </summary>
public class VictoryTableController : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    Camera mainCamera;

    private const float gravityValue = -8f;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("RunLevel"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.TurnOffPause;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");

                    Vault.instance.particleSystemVictoryBoard.gravityModifier = gravityValue;
                }
                if (hit.collider.CompareTag("RetryButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ResetLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");

                    Vault.instance.particleSystemVictoryBoard.gravityModifier = gravityValue;
                }
                if (hit.collider.CompareTag("SelectLevelButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.SelectLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");

                    Vault.instance.particleSystemVictoryBoard.gravityModifier = gravityValue;
                }
            }
        }
    }
}
