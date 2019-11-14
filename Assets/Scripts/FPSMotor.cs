using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    public event Action Land = delegate { };

    [SerializeField] Camera _camera = null;
    [SerializeField] float _cameraAngleLimit = 70f;
    [SerializeField] GroundDetector _groundDetector = null;
    //[SerializeField] GameObject barrel = null;

    Vector3 _movementThisFrame = Vector3.zero;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;
    private float _currentCameraRotationX = 0;
    private float _currentCameraRotationY = 0;
    //private Vector3 gunPosition;
    //private Quaternion gunRotation;
    bool _isGrounded = false;

    Rigidbody _rigidbody = null;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

    public void Sprint()
    {
        if(_isGrounded)
        {
            //_movementThisFrame *= 5;
        }
    }

    public void StopSprint()
    {
       // _movementThisFrame /= 5;
    }

    public void Move(Vector3 requestedMovement)
    {
        _movementThisFrame = requestedMovement;
    }

    public void Turn(float turnAmount)
    {
        _turnAmountThisFrame = turnAmount;
    }

    public void Look(float lookAmount)
    {
        _lookAmountThisFrame = lookAmount;
    }

    public void Jump(float jumpForce)
    {
        if (_isGrounded == false)
            return;
        _rigidbody.AddForce(Vector3.up * jumpForce);
        Debug.Log("jump");
    }

   



    private void FixedUpdate()
    {

        //gunPosition = barrel.transform.position;
        //gunRotation = _camera.transform.rotation;
        ApplySprint();
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
    }

    void ApplySprint()
    {
        Sprint();
    }

    void ApplyMovement(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero)
            return;
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
        _movementThisFrame = Vector3.zero;

    }

    void ApplyTurn(float rotateAmount)
    {
        if (rotateAmount == 0)
            return;
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
        _turnAmountThisFrame = 0;
    }

    void ApplyLook(float lookAmount)
    {
        if (lookAmount == 0)
            return;

        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp
            (_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);
        //--------------------
        /*
        _currentCameraRotationY -= lookAmount;
        _currentCameraRotationY = Mathf.Clamp
            (_currentCameraRotationY, -_cameraAngleLimit, _cameraAngleLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, _currentCameraRotationY, 0);
        */
        //--------------------
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        _lookAmountThisFrame = 0;
    }

    void OnGroundDetected()
    {
        _isGrounded = true;
        Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }

}
