using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject abilityStatusBar;

    private bool isDecreasing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (abilityStatusBar.gameObject.activeInHierarchy == false)
        {
            if (slider.value < 1 && isDecreasing == false)
            {
                slider.value += 0.01f;
            }
            else if ((slider.value > 0 && isDecreasing == true) || slider.value >= 1)
            {
                isDecreasing = true;
                slider.value -= 0.01f;
                //slider.value = 0;
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
