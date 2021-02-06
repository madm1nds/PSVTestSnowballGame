using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (slider.value < 1)
        {
            slider.value += 0.01f;
        }
        else
        {
            slider.value = 0;
        }

    }
}
