using System.Collections;
using System.Collections.Generic;
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
#pragma warning disable CS0618 // Тип или член устарел
                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8;
                }
                if (hit.collider.CompareTag("RetryButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.ResetLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");
#pragma warning disable CS0618 // Тип или член устарел
                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8;
                }
                if (hit.collider.CompareTag("SelectLevelButton"))
                {
                    AnimationActions.currentNameAnimation = AnimationActions.NameAnimation.SelectLevel;
                    Vault.instance.gameObjectVictoryBoard.GetComponent<Animator>().SetTrigger("Exit");
#pragma warning disable CS0618 // Тип или член устарел
                    Vault.instance.particleSystemVictoryBoard.gravityModifier = -8;
                }
            }
        }  
    }
    void ResetCurrentLevel()
    {

    }
}
