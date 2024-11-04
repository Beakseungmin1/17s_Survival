﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float curMoveSpeed;
    private float defaultspeed;
    public float RunnincreaseSpeed;
    public float JumpPower;
    private Vector2 curMoveMentInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public GameObject FirstCamera;
    public GameObject ThirdCamera;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;
    public bool isRun = false;

    public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        StopRun();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMoveMentInput.y + transform.right * curMoveMentInput.x;
        dir *= curMoveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }



    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveMentInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveMentInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && CharacterManager.Instance.Player.condition.Getstamina() > 1f)
        {
            defaultspeed = curMoveSpeed;
            curMoveSpeed += RunnincreaseSpeed;
            isRun = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveSpeed = defaultspeed;
            isRun = false;
        }
    }

    public void OnHotKey(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Output different values ​​for pressed keys
            if (context.action.name == "HotKeyNum1")
            {
                Debug.Log(1);
            }
            else if (context.action.name == "HotKeyNum2")
            {
                Debug.Log(2);
            }
            else if (context.action.name == "HotKeyNum3")
            {
                Debug.Log(3);
            }
            else if (context.action.name == "HotKeyNum4")
            {
                Debug.Log(4);
            }
            else if (context.action.name == "HotKeyNum5")
            {
                Debug.Log(5);
            }
        }
    }

    public void StopRun()
    {
        if (isRun && CharacterManager.Instance.Player.condition.Getstamina() <= 0f)
        {
            curMoveSpeed = defaultspeed;
            isRun = false;
        }
    }


    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.1f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.1f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (transform.right * 0.1f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + (-transform.right * 0.1f) + transform.up * 0.01f, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            // rays에 있는 레이의, 0.1f 길이만큼, groundlayermask 에 포함되는 레이어만 
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnChangeThirdView(InputAction.CallbackContext context)
    {
        if (isThird())
        {
            ThirdCamera.SetActive(false);
            FirstCamera.SetActive(true);
        }
        else
        {
            ThirdCamera.SetActive(true);
            FirstCamera.SetActive(false);
        }
    }

    public bool isThird()
    {
        return ThirdCamera.activeInHierarchy;
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }


}
