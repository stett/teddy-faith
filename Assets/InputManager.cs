using System;
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    // OBJECT HANDLES
    public Player _player;

    // player movement variables
    private float _h;       // horizontal movement
    private bool _jump;     // jump
    private float _v;       // vertical mouse movement

	// Use this for initialization
	void Start () 
    {
        // find the player
        if (_player == null)
	    {
            _player = FindObjectOfType<Player>();
            if (_player == null)
	        {
	            Debug.Log("Player couldn't be found.");
	        }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	}

    void Update()
    {
        // read input
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Mouse Y");
        _jump = Input.GetKeyDown(KeyCode.Space);
        
        // act on player
         _player.Move(_h);
        if(_jump) _player.Jump();

        // Deactivate current player target when mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get ray cast to mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "TeddyBear")
            {
                hit.transform.GetComponent<TeddyState>().SetAsTarget();
            }
            else if (_player.HasTarget())
            {
                _player.UnTarget();
            }
        }
    }
}
