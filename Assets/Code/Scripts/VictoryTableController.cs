using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTableController : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Мы попали в: " + hit.collider.gameObject.name);
                if (hit.collider.tag == "B1")
                {
                    
                }
            }
        }  
    }
}
