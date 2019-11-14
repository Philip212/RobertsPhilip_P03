using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]

public class PlayerController : MonoBehaviour
{
    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;
    //[SerializeField] GameObject projectile = null;

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.SprintInput += OnSprint;
        _input.SprintOffInput += OnStopSprint;
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.GrappleInput += OnGrapple;
    }


    private void OnDisable()
    {
        _input.SprintInput -= OnSprint;
        _input.SprintOffInput += OnStopSprint;
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.GrappleInput -= OnGrapple;
    }

    void OnSprint()
    {
        _motor.Sprint();
    }

    void OnStopSprint()
    {
        _motor.StopSprint();
    }

    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }


    void OnRotate(Vector3 rotation)
    {
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);

    }


    void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }

    void OnGrapple()
    {

    }
    
}
