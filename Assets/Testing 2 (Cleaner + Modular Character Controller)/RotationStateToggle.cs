using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RotationStateToggle : MonoBehaviour
{
    [SerializeField] private List<Vector3> rotationStates  = new List<Vector3>();
    private int index = 0;
    private int i;

    [SerializeField] private Volume graphics;
    private bool volClouds;
    void Start()
    {
        volClouds = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { index++; }
        i = index % rotationStates.Count;
        transform.rotation = Quaternion.Euler(rotationStates[i]);

        if (Input.GetKeyDown(KeyCode.C))
        {
            volClouds = !volClouds;
        }

        if (graphics.profile.TryGet<VolumetricClouds>(out var clouds))
        {
            clouds.active = volClouds;
        }
    }
}
