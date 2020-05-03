using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController _controller;
    private Joystick _joystick;
    private JumpButton _jumpButton;
    private float _yVelocity;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float _jumpHeight = 25;

    void Start() {
        _controller = GetComponent<CharacterController>();
        _joystick = FindObjectOfType<Joystick>();
        _jumpButton = FindObjectOfType<JumpButton>();
    }

    void Update() {
        CalculateMovement();
    }

    private void CalculateMovement() {
        float horizontalInput;
        bool isJumpPressed;
    #if (UNITY_ANDROID || UNITY_IOS)
        horizontalInput = _joystick.Horizontal;
        isJumpPressed = _jumpButton.pressed;
    #else
        horizontalInput = Input.GetAxis("Horizontal");
        isJumpPressed = Input.GetKeyDown(KeyCode.Space);
    #endif
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * speed;

        if (_controller.isGrounded) {
            if (isJumpPressed) {
                _yVelocity = _jumpHeight;
            }
        }
        else {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        
        _controller.Move(velocity * (speed * Time.deltaTime));
    }
}