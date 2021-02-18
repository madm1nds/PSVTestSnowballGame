using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject abilityStatusBar;

    private bool isDecreasing;
    private float speedThrowPower;
    void Start()
    {
        speedThrowPower = Vault.instance.settings.speedThrowPower / 100;
    }

    void Update()
    {
        if (PauseButtonController.isPause == false)
        {
            if (abilityStatusBar.activeInHierarchy == false)
            {
                if (slider.value < 1 && isDecreasing == false)
                {
                    slider.value += speedThrowPower;
                }
                else if ((slider.value > 0 && isDecreasing == true) || slider.value >= 1)
                {
                    isDecreasing = true;
                    slider.value -= speedThrowPower;
                }
                else
                {
                    isDecreasing = false;
                }
            }
            else
            {
                slider.value = 0;
            }
        }
    }
}
