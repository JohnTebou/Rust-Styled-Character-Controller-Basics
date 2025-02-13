using UnityEngine;

public class AnyToggle : MonoBehaviour
{
    [SerializeField] private GameObject hideIt;
    private bool hide;
    void Start()
    {
        hide = false;
    }

    // Update is called once per frame
    void Update()
    {
        hide = Input.GetKeyDown(KeyCode.H) ? !hide : hide;
        hideIt.SetActive(hide);
    }
}
