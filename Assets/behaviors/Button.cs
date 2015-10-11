using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool is_pressed { get; private set; }
    float original_y;
    Transform child;

    // Use this for initialization
    void Start () {
        is_pressed = false;
        child = transform.GetChild(0).transform;
        original_y = child.position.y;//transform.position.y;
    }

    // Update is called once per frame
    void Update () {

        float y = child.position.y;
        if (is_pressed)
            y += (original_y - .15f - y) * 0.1f;
        else
            y += (original_y - y) * 0.3f;

        child.position = new Vector3(child.position.x, y);
    }

    void OnTriggerStay(Collider other)
    {
        is_pressed = true;
    }

    void OnTriggerExit(Collider other)
    {
        is_pressed = false;
    }
}
