using UnityEngine;
using System.Collections;
//using UnityEditor;

public class BasicTeddyBehavior : MonoBehaviour
{
    private TeddyState state;

    // Use this for initialization
    void Start()
    {
        //body = GetComponent<Rigidbody>();
        //state = State.MOVING_RIGHT;
        state = GetComponent<TeddyState>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray_down = new Ray(transform.position, new Vector3(0.0f, -1.0f));

        if (state.state == TeddyState.State.MOVING_RIGHT) {
            RaycastHit hit;
            Ray ray_right = new Ray(transform.position, new Vector3(1.0f, 0.0f));
            Ray ray_right_down = new Ray(transform.position, new Vector3(1.0f, -1.0f));
            if ((Physics.Raycast(ray_right, out hit) && hit.distance < 0.5f && hit.transform.tag == "Ground") ||
                (Physics.Raycast(ray_down, 1.0f) && !Physics.Raycast(ray_right_down, 1.5f))) {
                state.state = TeddyState.State.MOVING_LEFT;
            }
        }

        else if (state.state == TeddyState.State.MOVING_LEFT) {
            RaycastHit hit;
            Ray ray_left = new Ray(transform.position, new Vector3(-1.0f, 0.0f));
            Ray ray_left_down = new Ray(transform.position, new Vector3(-1.0f, -1.0f));
            if ((Physics.Raycast(ray_left, out hit) && hit.distance < 0.5f && hit.transform.tag == "Ground") ||
                (Physics.Raycast(ray_down, 1.0f) && !Physics.Raycast(ray_left_down, 1.5f))) {
                state.state = TeddyState.State.MOVING_RIGHT;
            }
        }
    }
}
