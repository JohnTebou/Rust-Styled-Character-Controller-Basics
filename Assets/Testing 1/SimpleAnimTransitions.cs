using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimTransitions : MonoBehaviour
{
    [SerializeField] private List<Animator> animators = new List<Animator>();
    private int state;
    private bool throww;
    private PlayerInputHandler ih;

    void Start()
    {
        ih = gameObject.GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // set state based on inputs (whether or not character is moving or sprinting, and whether or not there is forward momentum,)
        if (ih.MoveInput.magnitude != 0)
        {
            if (ih.SprintValue != 0 && ih.MoveInput.y > 0)
            {
                state = 2;
            }
            else
            {
                state = 1;
            }
        }
        else
        {
            state = 0;
        }

        throww = ih.ThrowValue != 0 ? true : false;

        // set state
        foreach(Animator i in animators)
        {
            i.SetInteger("state", state);
            i.SetBool("throw", throww);
        }
    }
}
