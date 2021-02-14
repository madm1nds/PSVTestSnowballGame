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
    private StatusBarAbilityController statusBarAbilityController;
    


    private Vector2 direction;
    private float acceleration;    

    private const int notSnowball = 1;
    private GameObject[] hippoSnowballSetArray;
    private Transform[] hippoSnowballSetTransform;
    private Rigidbody2D[] hippoSnowballSetRigidbody2D;
    void Start()
    {
        hippoSnowballSetArray = new GameObject[hippoSnowballSet.transform.childCount- notSnowball];
        hippoSnowballSetTransform = new Transform[hippoSnowballSet.transform.childCount- notSnowball];
        hippoSnowballSetRigidbody2D = new Rigidbody2D[hippoSnowballSet.transform.childCount- notSnowball];
        for (int i = 0; i < hippoSnowballSetArray.Length; i++)
        {
            hippoSnowballSetArray[i] = hippoSnowballSet.transform.GetChild(i).gameObject;
            hippoSnowballSetTransform[i] = hippoSnowballSet.transform.GetChild(i).transform;
            hippoSnowballSetRigidbody2D[i] = hippoSnowballSet.transform.GetChild(i).GetComponent<Rigidbody2D>();
        }

        snowballButton.onClick.AddListener(delegate { ThrowSnowball(); });
        acceleration = 1;
        acceleration *= 1000f;
        direction = new Vector2(0.6f, 1f);
    }
    
    IEnumerator Attack(GameObject snowball)
    {
        float timer = 0f;
        Rigidbody2D rb = snowball.GetComponent<Rigidbody2D>();
        do
        {
            if (PauseButtonController.isPause == false)
            {
                rb.simulated = true;
                timer += 0.018f;
            }
            else
            {
                rb.simulated = false;
            }
            yield return new WaitForSeconds(0.001f);
        }
        while (snowball.activeInHierarchy == true && timer <=4);        
        snowball.SetActive(false);
        yield break;
    }

    private void ThrowSnowball()
    {
        if (!StatusBarAbilityController.isThrow)
        {
            for (int i = 0; i < hippoSnowballSetArray.Length; i++)
            {
                if (hippoSnowballSetArray[i].activeInHierarchy == false)
                {
                    statusBarAbilityController.InvokeChangeStatus();
                    StartCoroutine(Attack(hippoSnowballSetArray[i]));                    

                    hippoSnowballSetTransform[i].position = spawnPlace.position;                    
                    hippoSnowballSetArray[i].SetActive(true);
                    hippoSnowballSetRigidbody2D[i].mass = 3f;

                    if (slider.value < 0.2f)
                    {
                        direction = new Vector2(0.6f, 1f);
                        hippoSnowballSetRigidbody2D[i].mass -= slider.value;                        
                    }
                    else if (slider.value < 0.4f)
                    {
                        hippoSnowballSetRigidbody2D[i].mass -= slider.value + 0.2f;
                        direction = new Vector2(0.65f, 1f);
                    }
                    else if (slider.value < 0.6f)
                    {
                        direction = new Vector2(0.75f, 0.9f);
                        hippoSnowballSetRigidbody2D[i].mass -= slider.value + 0.4f;
                    }
                    else if (slider.value < 0.8f)
                    {
                        direction = new Vector2(0.8f, 0.85f);
                        hippoSnowballSetRigidbody2D[i].mass -= slider.value + 0.6f;
                    }
                    else if (slider.value <= 1f)
                    {
                        direction = new Vector2(0.85f, 0.8f);
                        hippoSnowballSetRigidbody2D[i].mass -= slider.value + 0.8f;
                    }
                    hippoSnowballSetRigidbody2D[i].AddForce(direction.normalized * acceleration);

                    //чем меньше радиус коллайдера, тем сильнее закручивается снежок.
                    hippoSnowballSetRigidbody2D[i].AddTorque(45);
                    break;
                }
            }
        }
    }

}
