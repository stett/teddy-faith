using UnityEngine;
using System.Collections;

public class BlindTeddy : MonoBehaviour
{
    private TeddyState state;
    private Player player;
    public float max_dist_to_player = 6.0f;
    public float trigger_dist_to_player = 4.0f;

    private FollowSelectedTeddy _wisp;

    // Use this for initialization
    void Start()
    {
        //body = GetComponent<Rigidbody>();
        //state = State.MOVING_RIGHT;
        state = GetComponent<TeddyState>();
        player = GameObject.FindObjectOfType<Player>();//GetGame<Player>();
        state.selectable = false;

        _wisp = FindObjectOfType<FollowSelectedTeddy>();
    }

    // Update is called once per frame
    void Update()
    {

        // SPECIAL FOR THE TUTORIAL TEDDY
        // TODO: MAKE IT NOT HARD CODED... unless we don't actually have to
        state.selectable = false;
        state.facing = TeddyState.Facing.RIGHT;



        Ray ray_down = new Ray(transform.position, new Vector3(0.0f, -1.0f));
        float distance = (transform.position - player.transform.position).magnitude;

        //Debug.Log(distance + " > " + max_dist_to_player + " : " + (distance > max_dist_to_player));
        if (distance > max_dist_to_player)
        {
            state.state = TeddyState.State.WAITING;
            _wisp.UnSetTarget();
        } 
        else if (distance < trigger_dist_to_player)
        {
            _wisp.SetTarget(gameObject);
            if (state.facing == TeddyState.Facing.RIGHT)
                state.state = TeddyState.State.MOVING_RIGHT;
            else
                state.state = TeddyState.State.MOVING_LEFT;
        }
    }
}
