using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Spine.Unity;

public class PauseButtonController : MonoBehaviour
{
    public static bool isPause;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject screenLock;
    [SerializeField]
    private GameObject enemies;

    private SkeletonAnimation[] animationEnemies;
    // Start is called before the first frame update
    void Start()
    {
        animationEnemies = new SkeletonAnimation[enemies.transform.childCount];
        for (int i = 0; i < animationEnemies.Length; i++)
        {
            animationEnemies[i] = enemies.transform.GetChild(i).GetComponent<SkeletonAnimation>();
        }
        isPause = false;
        pauseButton.onClick.AddListener(delegate { clickOnPause(); });
    }

    void clickOnPause()
    {

        if (isPause == false)
        {
            for (int i = 0; i < animationEnemies.Length; i++)
            {
                if (enemies.transform.GetChild(i).gameObject.activeInHierarchy == true)
                {
                    animationEnemies[i].AnimationName = "Idle";
                }
            }
            screenLock.SetActive(true);
        }
        else
        {
            screenLock.SetActive(false);
        }
        isPause = !isPause;
    }
}
