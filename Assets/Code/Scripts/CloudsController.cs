using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{
    [SerializeField]
    private Transform clouds;

    private bool isMoveLeft;
    
    void FixedUpdate()
    {
        if (isMoveLeft == false && clouds.position.x <= 110f)
        {
            clouds.position = new Vector3(clouds.position.x + 0.005f, clouds.position.y, clouds.position.z);
        }
        else
        {
            isMoveLeft = true;
        }
        if (isMoveLeft == true && clouds.position.x >= -110f)
        {
            clouds.position = new Vector3(clouds.position.x - 0.005f, clouds.position.y, clouds.position.z);
        }
        else
        {
            isMoveLeft = false;
        }
    }
}
