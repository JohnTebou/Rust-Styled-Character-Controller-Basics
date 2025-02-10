using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSway : MonoBehaviour
{
    private PlayerInputHandler ih;

    [Header("Sway Controls")]
    [SerializeField] private Transform orienter;
    [SerializeField] private Transform swayer;
    [SerializeField] private Vector2 swayMultiplier;
    [SerializeField] private float swaySmoothing;

    void Start()
    {
        ih = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion yRot = Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X") * -swayMultiplier.x, orienter.up);
        Quaternion xRot = Quaternion.AngleAxis(Input.GetAxisRaw("Mouse Y") * swayMultiplier.y, orienter.right);

        Quaternion targRot = xRot * yRot;

        swayer.localRotation = Quaternion.Slerp(swayer.localRotation, targRot, swaySmoothing * Time.deltaTime);
    }
}
