using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSettings : MonoBehaviour
{
    [SerializeField] 
    private Settings settings;
    public static int healthPoints;

    void Start()
    {
        settings.ApplySettings();
        healthPoints = 3;
    }
}
