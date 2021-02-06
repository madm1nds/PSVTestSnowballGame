using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnowballButton : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject hippoSnowballSet;
    [SerializeField]
    private float time;

    private Vector2 direction;
    private float acceleration;
    private bool isThrow = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ThrowSnowball(); });
        acceleration = 1;
        acceleration *= 1000f;
        direction = new Vector2(0.6f, 1f);
    }

    IEnumerator RunTimer(GameObject snowball)
    {
        yield return new WaitForSeconds(time);
        isThrow = false;
        yield return new WaitForSeconds(4f);
        snowball.SetActive(false);
        StopCoroutine(RunTimer(snowball));
    }

    private void ThrowSnowball()
    {
        if (!isThrow)
        {
            for (int i = 0; i < hippoSnowballSet.transform.childCount; i++)
            {
                if (hippoSnowballSet.transform.GetChild(i).gameObject.activeInHierarchy == false)
                {
                    StartCoroutine(RunTimer(hippoSnowballSet.transform.GetChild(i).gameObject)); ;
                    hippoSnowballSet.transform.GetChild(i).transform.position = player.transform.position;
                    isThrow = true;
                    hippoSnowballSet.transform.GetChild(i).gameObject.SetActive(true);
                    hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass = 3f;

                    if (slider.value < 0.2f)
                    {
                        direction = new Vector2(0.6f, 1f);
                        hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass -= slider.value;                        
                    }
                    else if (slider.value < 0.4f)
                    {
                        hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass -= slider.value + 0.2f;
                        direction = new Vector2(0.65f, 1f);
                    }
                    else if (slider.value < 0.6f)
                    {
                        direction = new Vector2(0.75f, 0.9f);
                        hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass -= slider.value + 0.4f;
                    }
                    else if (slider.value < 0.8f)
                    {
                        direction = new Vector2(0.8f, 0.85f);
                        hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass -= slider.value + 0.6f;
                    }
                    else if (slider.value <= 1f)
                    {
                        direction = new Vector2(0.85f, 0.8f);
                        hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().mass -= slider.value + 0.8f;
                    }

                    hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(direction.normalized * acceleration);
                    break;
                }
            }
        }
    }

}
