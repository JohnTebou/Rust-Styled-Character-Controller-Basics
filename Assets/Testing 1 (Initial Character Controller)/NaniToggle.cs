using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaniToggle : MonoBehaviour
{
    [Header("Animator Controllers")]
    [SerializeField] private List<Animator> controllers = new List<Animator>(3);

    public bool nani;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Animator i in controllers)
        {
            i.SetBool("nani", nani);
        }

        nani = Input.GetKey(KeyCode.LeftShift) ? true : false;
    }
}
