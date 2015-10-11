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
        if (target)
        {

            Vector3 target_pos = target.transform.position;
            if (target.tag == "Player")
            {
                target_pos += new Vector3(0, 2, 0);
            }
            Vector3 diff = target_pos - transform.position;

            float dist = diff.magnitude;
            diff.Normalize();
            diff *= 10.0f * dist;
            body.AddForce(diff);

            // if close deactivate
            if (dist < 1.2f)
            {
                GetComponentInChildren<ParticleSystem>().Stop();
            }
            else
            {
                GetComponentInChildren<ParticleSystem>().Play();
                
            }


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
