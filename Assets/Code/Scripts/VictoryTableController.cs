using UnityEngine;

public class VictoryTableController : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
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

                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8f; 
                }
                if (hit.collider.CompareTag("RetryButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ResetLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");

                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8f;
                }
                if (hit.collider.CompareTag("SelectLevelButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.SelectLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");
                    
                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8f;
                }
            }
        }  
    }
}
