using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnowballButton : MonoBehaviour
{
    [SerializeField]
    private Button snowballButton;
    [SerializeField]
    private Transform spawnPlace;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject hippoSnowballSet;
    [SerializeField]
    private GameObject abilityStatusBar;
    [SerializeField]
    private GameObject abilityStatusBarMask;
    [SerializeField]
    private GameObject readySnowball;
    [SerializeField]
    private Snowball time;


    private Vector2 direction;
    private float acceleration;
    private bool isThrow = false;

    void Start()
    {
        snowballButton.onClick.AddListener(delegate { ThrowSnowball(); });
        acceleration = 1;
        acceleration *= 1000f;
        direction = new Vector2(0.6f, 1f);
    }
    //корутин продолжает работать, даже после выключение объекта...
    IEnumerator RunTimer(GameObject snowball)
    {
        readySnowball.SetActive(false);
        abilityStatusBarMask.transform.localPosition = new Vector3(0f, abilityStatusBarMask.transform.localPosition.y, abilityStatusBarMask.transform.localPosition.z);
        abilityStatusBar.SetActive(true);
        yield return new WaitForSeconds(time.cooldown);
        isThrow = false;
        readySnowball.SetActive(true);
        abilityStatusBar.SetActive(false);
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
                    hippoSnowballSet.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    hippoSnowballSet.transform.GetChild(i).gameObject.GetComponent<CircleCollider2D>().enabled = true;

                    StartCoroutine(RunTimer(hippoSnowballSet.transform.GetChild(i).gameObject));
                    hippoSnowballSet.transform.GetChild(i).transform.position = spawnPlace.transform.position;
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

                    //чем меньше радиус коллайдера, тем сильнее закручивается снежок.
                    hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>().AddTorque(45);
                    break;
                }
            }
        }
    }

}
