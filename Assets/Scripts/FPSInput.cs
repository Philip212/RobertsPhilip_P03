﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;
    [SerializeField] bool _invertHorizontal = false;
    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action GrappleInput = delegate { };
    public event Action SprintInput = delegate { };
    public event Action SprintOffInput = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectSprintInput();
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectGrappleInput();
    }

    void DetectSprintInput()
    {
        /*bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        if(sprintInput != false)
        {
            //Debug.Log("Sprint on");
            SprintInput?.Invoke();
        }*/
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintInput?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprintOffInput?.Invoke();
        }
    }


    void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if(xInput != 0 || yInput != 0)
        {
            //Convert to local directions, based on player orientation.
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;
            //Combine movements into a single vector.
            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if(xInput != 0 || yInput != 0)
        {
            if(_invertVertical == true)
            {
                yInput = -yInput;
            }
            if(_invertHorizontal == true)
            {
                xInput = -xInput;
            }
            Vector3 rotation = new Vector3(yInput, xInput, 0);
            RotateInput?.Invoke(rotation);
        }
    }

    void DetectJumpInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }

    void DetectGrappleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GrappleInput?.Invoke();
        }
    }
}
