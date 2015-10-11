using UnityEngine;
using System.Collections;

public class TeddyState : MonoBehaviour
{
    public enum State
    {
        DEAD,
        WAITING,
        MOVING_RIGHT,
        MOVING_LEFT,
    };

    public enum Facing
    {
        RIGHT,
        LEFT,
    };

    public float WALK_SPEAD = 2.0f;
    public State state = State.MOVING_RIGHT;
    public bool selectable = true;
    public Facing facing = Facing.RIGHT;
    public float max_living_dist_from_player = 15.0f;
    private Rigidbody body;
    private Player player;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray_down = new Ray(transform.position, new Vector3(0.0f, -1.0f));

        if (state == State.DEAD) {
            // Dead bears don't do shit.

        } else if (state == State.WAITING) {
            body.velocity = new Vector3(0.0f, body.velocity.y);

        } else if (state == State.MOVING_RIGHT) {
            body.velocity = new Vector3(WALK_SPEAD, body.velocity.y);
            facing = Facing.RIGHT;

        } else if (state == State.MOVING_LEFT) {
            body.velocity = new Vector3(-WALK_SPEAD, body.velocity.y);
            facing = Facing.LEFT;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            FindObjectOfType<FollowSelectedTeddy>().UnSetTarget();
            Destroy(gameObject);
        }
    }

    void OnMouseEnter()
    {
        if (!selectable)
            return;

        Player _player = FindObjectOfType<Player>();
        if (_player)
            if (!_player.HasTarget()) Highlight();
    }

    void OnMouseExit()
    {
        if (!selectable)
            return;

        Player _player = FindObjectOfType<Player>();
        if (_player)
            if (!_player.HasTarget()) UnHighlight();
    }

    public void SetAsTarget()
    {
        if (!selectable)
            return;

        Debug.Log("OnMouseDown");
        Player _player = FindObjectOfType<Player>();
        Highlight();
        if (_player)
        {
            if (!_player.HasTarget())
            {
                _player.Target = gameObject;

                if (facing == Facing.RIGHT)
                    state = State.MOVING_RIGHT;
                else
                    state = State.MOVING_LEFT;
            }
        }
    }

    void Highlight()
    {
        FollowSelectedTeddy wisp = FindObjectOfType<FollowSelectedTeddy>();
        if (wisp) wisp.SetTarget(gameObject);
    }

    void UnHighlight()
    {
        FollowSelectedTeddy wisp = FindObjectOfType<FollowSelectedTeddy>();
        if (wisp) wisp.UnSetTarget();
    }
}
