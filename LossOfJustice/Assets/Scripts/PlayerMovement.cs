using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput input;
    public CharacterController controller;
    public Transform cam;

    public Animator anim;

    [SerializeField] private Vector2 _move;
    public Vector2 _look;
    [SerializeField] private float _sprintValue;
    [SerializeField] private float _moveSpeed;
    public float aimValue;
    public float fireValue;

    [SerializeField] private float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    // Physics
    public float gravity = -9.81f;
    [SerializeField] Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    bool isGrounded;

    public float rotationPower = 3f;

    // Weapons to Use
    public int levelWeapon;

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        _sprintValue = value.Get<float>();
    }

    public void OnAim(InputValue value)
    {
        aimValue = value.Get<float>();
    }

    public void OnFire(InputValue value)
    {
        fireValue = value.Get<float>();
    }

    public GameObject followTransform;


    private void Start()
    {
        CursorControl();
    }

    private void CursorControl()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        #region Follow Transform Rotation

        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var _angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (_angle > 180 && _angle < 340)
        {
            angles.x = 340;
        }
        else if (_angle < 180 && _angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
        #endregion


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 direction = new Vector3(_move.x, 0f, _move.y).normalized;

        AnimationControl();

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(_sprintValue == 1f)
            {
                controller.Move(moveDir.normalized * (_moveSpeed * 2f) * Time.deltaTime);
            }
            else if(_sprintValue != 1f)
            {
                controller.Move(moveDir.normalized * _moveSpeed * Time.deltaTime);
            }
        }

        if (aimValue == 1)
        {
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        //Set the player rotation based on the look transform
        //transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void AnimationControl()
    {
        anim.SetBool("isMoving", _move.magnitude > 0.01f ? true : false);
        anim.SetBool("isSprinting", _sprintValue == 1f ? true : false);
    }
}