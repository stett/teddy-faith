using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>().gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player == null)
            return;
        Vector3 pos = transform.position;
        Vector3 targ = player.transform.position;
        targ = new Vector3(targ.x, targ.y + 3.0f);
        pos += (targ - pos) * 0.1f;
        pos.z = transform.position.z;
        transform.position = pos;
	}
}
