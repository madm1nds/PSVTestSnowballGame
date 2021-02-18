using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс управляющий поведением слайдера силы броска.
/// Увеличивает значение от нуля до одного и обратно.
/// </summary>
public class SliderController : MonoBehaviour
{
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
            if (Vault.instance.gameObjectStatusBarAbility.activeInHierarchy == false)
            {
                if (Vault.instance.sliderUISlider.value < 1 && isDecreasing == false)
                {
                    Vault.instance.sliderUISlider.value += speedThrowPower;
                }
                else if ((Vault.instance.sliderUISlider.value > 0 && isDecreasing == true) || Vault.instance.sliderUISlider.value >= 1)
                {
                    isDecreasing = true;
                    Vault.instance.sliderUISlider.value -= speedThrowPower;
                }
                else
                {
                    isDecreasing = false;
                }
            }
            else
            {
                Vault.instance.sliderUISlider.value = 0;
            }
        }
    }
}
