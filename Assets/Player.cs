using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////
    // editor variables
    [SerializeField]                    private Transform _head; 
    [SerializeField][Range(1f, 10f)]    private float _moveSpeed;
    [SerializeField][Range(1f, 10f)]    private float _jumpForce;
    [SerializeField]                    private FollowSelectedTeddy _wisp_prefab;

    // private variables
    private bool _grounded;
    private bool _direction;    // true : right | false : left
    private bool _channeling;   // is character channeling life to bears?
    private bool _jumping;
    private GameObject _target;
    private FollowSelectedTeddy _wisp;
    private Animator _animator;

    // getters and setters
    public bool Grounded
    {
        get { return _grounded; }
    }

    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }


    ////////////////////////////////////////////////////////////////////
    /// UNITY FUNCTIONS
    ////////////////////////////////////////////////////////////////////
	void Start ()
    {
        _channeling = false;
        _direction = true;
        _grounded = false;
        _target = null;
        _jumping = false;

        // Instantiate wisp
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1.0f);
        _wisp = Instantiate(_wisp_prefab, pos, Quaternion.identity) as FollowSelectedTeddy;
        //Debug.Log(_wisp);

        _animator = GetComponentInChildren<Animator>();
    }
	
	void Update () 
    {
        // look at the mouse (rotate head to face the mouse pointer)
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = mousePos - pos;
        _head.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg));

        // Check to see if we're grounded
        RaycastHit hit;
        LayerMask mask = 1;//~LayerMask.NameToLayer("Bears");
        if ((Physics.Raycast(new Ray(transform.position, new Vector3(0.5f, -1.0f)),  out hit, mask) && hit.distance < 0.7f) ||
            (Physics.Raycast(new Ray(transform.position, new Vector3(0.0f, -1.0f)),  out hit, mask) && hit.distance < 0.7f) ||
            (Physics.Raycast(new Ray(transform.position, new Vector3(-0.5f, -1.0f)), out hit, mask) && hit.distance < 0.7f)) {
            if (!_grounded)
            {
                _animator.SetBool("isJump", false);
                _jumping = false;
            }
            _grounded = true;
        } else {
            
            _grounded = false;
        }


        // Unselect the current target if it's dead or out of range
        if (_target)
        {
            TeddyState target_state = _target.GetComponent<TeddyState>();
            if (target_state && target_state.state == TeddyState.State.DEAD ||
                (transform.position - _target.transform.position).magnitude > target_state.max_living_dist_from_player)
            {
                UnTarget();
            }
        }
    }

    /*
    // Collisions
    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collision Enter: " + other.transform.name);
        if (other.transform.tag == "Ground")
        {
             _grounded = true;
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Death")
        {
            Application.LoadLevel("main");
        }
    }

    ////////////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS
    ////////////////////////////////////////////////////////////////////
    public void Move(float h)  
    {
        // set the direction: h>0 --> true || h==0 --> keep value || h<0 false
        //_direction = h > 0 || (h<Single.Epsilon && _direction);
        if (Mathf.Abs(h) > Single.Epsilon && !_jumping)
        {
            _direction = h > Single.Epsilon;

            // animation
            _animator.SetBool("isWalking", true);

            // move the object
            //if(_grounded)   transform.Translate(transform.right * _moveSpeed * Time.deltaTime * h);
            if (_grounded)
            {
                transform.rotation = _direction ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                Rigidbody body = GetComponent<Rigidbody>();
                body.velocity = new Vector3(_moveSpeed * h, body.velocity.y);
            }
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    public void Jump()
    {
        StartCoroutine("JumpTrigger");
    }

    IEnumerator JumpTrigger()
    {
        if (_grounded && !_channeling)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _animator.SetBool("isJump", true);
            _jumping = true;

            yield return new WaitForSeconds(1);

            Vector3 force = _direction ? new Vector3(1, 1, 0) : new Vector3(-1, 1, 0);
            force *= _jumpForce;
            GetComponent<Rigidbody>().velocity = force;

        }
    }

    public bool HasTarget()
    {
        return _target != null;
    }

    public void UnTarget()
    {
        TeddyState teddy_state = _target.GetComponent<TeddyState>();
        if (teddy_state)
            teddy_state.state = TeddyState.State.DEAD;
        _target = null;
        _wisp.UnSetTarget();
    }
}
