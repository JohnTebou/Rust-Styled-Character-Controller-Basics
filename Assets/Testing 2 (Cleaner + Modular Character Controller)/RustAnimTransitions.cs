using UnityEngine;

public class RustAnimTransitions : MonoBehaviour
{
    private Animator a;
    private PlayerInputHandler ih;

    [Header("FOV Controls")]
    [SerializeField] private Camera cam;
    private float targetFOV;
    [SerializeField] private float baseFOV;
    private int state;
    private bool throww;
    [SerializeField] private float ADSFOV;
    [SerializeField] private float FOVChangeSpeed;
    void Start()
    {
        a = GetComponent<Animator>();
        ih = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {

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

        a.SetBool("throw", Input.GetKey(KeyCode.Mouse1) ? true : false);
        a.SetInteger("state", state);

        targetFOV = Input.GetKey(KeyCode.Mouse1) ? ADSFOV : baseFOV;
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetFOV, FOVChangeSpeed * Time.deltaTime);
    }
}
