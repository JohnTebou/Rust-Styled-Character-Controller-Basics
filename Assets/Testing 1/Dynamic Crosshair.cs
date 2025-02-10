using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    private Animator anyValidAnimator;
    private float state;

    [Header("UI Elements")]
    [SerializeField] private List<Image> hairs = new List<Image>();

    [Header("Dynamism Parameters")]
    [SerializeField] private float minDistance;
    [SerializeField] private float midDistance;
    [SerializeField] private float maxDistance;
    private float targetDistance;
    [Space(5)]
    [SerializeField] private float crosshairShiftSpeed;

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


        // handle target crosshair offset based on state (closest for idle, intermediate for walking, farthest for running)
        switch (state)
        {
            case 0:
                targetDistance = minDistance;
                break;
            case 1:
                targetDistance = midDistance;
                break;
            case 2:
                targetDistance = maxDistance;
                break;
        }

        // handle interpolation of crosshairs
        hairs[0].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[0].rectTransform.anchoredPosition, new Vector2(-targetDistance, 0), crosshairShiftSpeed * Time.deltaTime);
        hairs[1].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[1].rectTransform.anchoredPosition, new Vector2(targetDistance, 0), crosshairShiftSpeed * Time.deltaTime);
        hairs[2].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[2].rectTransform.anchoredPosition, new Vector2(0, -targetDistance), crosshairShiftSpeed * Time.deltaTime);
        hairs[3].rectTransform.anchoredPosition = Vector2.MoveTowards(hairs[3].rectTransform.anchoredPosition, new Vector2(0, targetDistance), crosshairShiftSpeed * Time.deltaTime);

        // toggle T-crosshair
        hairs[3].gameObject.SetActive(Input.GetKeyDown(KeyCode.T) ? !hairs[3].gameObject.activeSelf : hairs[3].gameObject.activeSelf); 
    }
}
