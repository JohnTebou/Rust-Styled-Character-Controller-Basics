using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    [SerializeField] private Transform targ;
    [SerializeField] Vector3 offsets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targ.position;
        transform.rotation = Quaternion.Euler(targ.rotation + offsets);
    }
}
