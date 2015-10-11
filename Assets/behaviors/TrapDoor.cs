using UnityEngine;
using System.Collections;

public class TrapDoor : MonoBehaviour {

    public Button button;
    private BoxCollider box_collider;
    private float rot_original;
    private float x_original;

    // Use this for initialization
    void Start () {
        box_collider = GetComponent<BoxCollider>();
        rot_original = transform.rotation.eulerAngles.z;
        x_original = transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        //box_collider.enabled = !button.is_pressed;

        float x_targ = (button.is_pressed ? x_original + transform.localScale.x : x_original);
        float x = transform.position.x;
        float diff = x_targ - x;
        x += diff * .3f;
        //transform.eulerAngles = new Vector3(0.0f, 0.0f, rotation);
        transform.position = new Vector3(x, transform.position.y);

        /*
        float rotation_target = (button.is_pressed ? rot_original + 90.0f : rot_original);
        float rotation = transform.rotation.eulerAngles.z;
        float diff = rotation_target - rotation;
        rotation += diff * .2f;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rotation);
        */
        //box_collider.enabled = Mathf.Abs(diff) < 0.1f;
    }
}
