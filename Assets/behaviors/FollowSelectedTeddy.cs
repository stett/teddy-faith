using UnityEngine;
using System.Collections;

public class FollowSelectedTeddy : MonoBehaviour {

    private GameObject target = null;
    private Rigidbody body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        UnSetTarget();
    }

    // Update is called once per frame
    void Update () {

        // Apply force towards the teddy
        if (target) {
            Vector3 force = target.transform.position - transform.position;
            float dist = force.magnitude;
            force.Normalize();
            force *= 10.0f * dist;
            body.AddForce(force);
        }
    }

    public void SetTarget(GameObject targ)
    {
        target = targ;
    }

    public void UnSetTarget()
    {
        Player _player = FindObjectOfType<Player>();
        if (_player) target = _player.gameObject;
    }
}
