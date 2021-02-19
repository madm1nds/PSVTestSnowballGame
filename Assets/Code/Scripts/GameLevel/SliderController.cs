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
    private const int maxValue = 1;
    private const int minValue = 0;
    private const float correctionSpeedThrowPower = 100;
    void Start()
    {
        speedThrowPower = Vault.instance.settings.speedThrowPower / correctionSpeedThrowPower;
    }

    void Update()
    {        
        if (PauseButtonController.isPause == false)
        {
            if (Vault.instance.gameObjectStatusBarAbility.activeInHierarchy == false)
            {
                if (Vault.instance.sliderUISlider.value < maxValue && isDecreasing == false)
                {
                    Vault.instance.sliderUISlider.value += speedThrowPower;
                }
                else if ((Vault.instance.sliderUISlider.value > minValue && isDecreasing == true) || Vault.instance.sliderUISlider.value >= maxValue)
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
                Vault.instance.sliderUISlider.value = minValue;
            }
        }
    }
}
