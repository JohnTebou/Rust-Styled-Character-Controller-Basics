using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    private Animator anyValidAnimator;
    private float state;

    [Header("UI Elements")]
    [SerializeField] private List<Image> hairs = new List<Image>(4);

    [Header("Dynamism Parameters")]
    [SerializeField] private float throwDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float midDistance;
    [SerializeField] private float maxDistance;
    [Space(5)]
    [SerializeField] private float correctionX;
    [SerializeField] private float correctionY;
    private float targetDistance;
    private bool throww;
    [Space(5)]
    [SerializeField] private float crosshairShiftSpeed;
    [SerializeField] private float throwShiftSpeed;
    private float shiftSpeed;
    [Space(5)]
    private float targetExtense;
    [SerializeField] private float defaultExtense;
    [SerializeField] private float crossExtense;
    [SerializeField] private float ADSDownscaleMultiplier;
    [SerializeField] private float widthShiftSpeed;

    private void Awake()
    {
        anyValidAnimator = GameObject.FindGameObjectWithTag("validAnimator").GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // get state which will be used to determine crosshair distance
        state = anyValidAnimator.GetInteger("state");

        throww = anyValidAnimator.GetBool("throw");


        // handle target crosshair offset based on state (closest for idle, intermediate for walking, farthest for running, custom for spear throw)
        switch (state)
        {
            case 0:
                if (!throww) { targetDistance = minDistance; }
                break;
            case 1:
                if (!throww) { targetDistance = midDistance; }
                break;
            case 2:
                if (!throww) { targetDistance = maxDistance; }
                break;
        }
        if (throww) { targetDistance = throwDistance;  }

        shiftSpeed = throww ? throwShiftSpeed : crosshairShiftSpeed;

        targetExtense = throww ? defaultExtense * ADSDownscaleMultiplier : defaultExtense;

        SetWidth(hairs[0], targetExtense, crossExtense, widthShiftSpeed, true);
        SetWidth(hairs[1], targetExtense, crossExtense, widthShiftSpeed, true);
        SetWidth(hairs[2], targetExtense, crossExtense, widthShiftSpeed, false);
        SetWidth(hairs[3], targetExtense, crossExtense, widthShiftSpeed, false);
        hairs[4].rectTransform.sizeDelta = new Vector2(crossExtense, crossExtense);



        // handle interpolation of crosshairs
        hairs[0].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[0].rectTransform.anchoredPosition, new Vector2(-targetDistance + correctionX, 0), shiftSpeed * Time.deltaTime);
        hairs[1].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[1].rectTransform.anchoredPosition, new Vector2(targetDistance + correctionX, 0), shiftSpeed * Time.deltaTime);
        hairs[2].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[2].rectTransform.anchoredPosition, new Vector2(0, -targetDistance + correctionY), shiftSpeed * Time.deltaTime);
        hairs[3].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[3].rectTransform.anchoredPosition, new Vector2(0, targetDistance + correctionY), shiftSpeed * Time.deltaTime);

        // toggle T-crosshair
        hairs[3].gameObject.SetActive(Input.GetKeyDown(KeyCode.T) ? !hairs[3].gameObject.activeSelf : hairs[3].gameObject.activeSelf); 
    }

    private void SetWidth(Image i, float w, float c, float s, bool horizontal)
    {
        Vector2 rsd = i.rectTransform.sizeDelta;
        if (horizontal)
        {
            i.rectTransform.sizeDelta = Vector2.MoveTowards(rsd, new Vector2(w, c), widthShiftSpeed * Time.deltaTime);
        }
        else
        {
            i.rectTransform.sizeDelta = Vector2.MoveTowards(rsd, new Vector2(c, w), widthShiftSpeed * Time.deltaTime);
        }
    }
}
