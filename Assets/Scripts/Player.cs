using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private CharacterController _controller;
    private Joystick _joystick;
    private JumpButton _jumpButton;
    private bool _isDoubleJumpAllowed;
    private float _yVelocity;
    private UiManager _uiManager;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float _jumpHeight = 25;
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _lives = 3;
    private Vector3 _initialPosition;
    private const float _deadZone = -16f;

    void Start() {
        _controller = GetComponent<CharacterController>();
        _joystick = FindObjectOfType<Joystick>();
        _jumpButton = FindObjectOfType<JumpButton>();
        _uiManager = FindObjectOfType<UiManager>();
        _uiManager.UpdateLives(_lives);
        _uiManager.UpdateCoins(_coins);
        _initialPosition = transform.position;
        }

    void Update() {
        CalculateMovement();

        if (transform.position.y < _deadZone) {
            Respawn();
            }
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
                _isDoubleJumpAllowed = true;
                _yVelocity = _jumpHeight;
                }
            }
        else {
            if (_isDoubleJumpAllowed && isJumpPressed) {
                _yVelocity += _jumpHeight;
                _isDoubleJumpAllowed = false;
                }

            _yVelocity -= _gravity;

            }

        velocity.y = _yVelocity;

        _controller.Move(velocity * (speed * Time.deltaTime));
        }

    private void Respawn() {
        _controller.enabled = false;
        _lives--;

        transform.position = _initialPosition;

        if (_lives < 1) {
            SceneManager.LoadScene(0);
            _lives = 3;
            }

        _uiManager.UpdateLives(_lives);

        StartCoroutine(RespawnRoutine());
        }

    IEnumerator RespawnRoutine() {
        yield return new WaitForSeconds(0.1f);
        _controller.enabled = true;
        }

    public void UpdateCoins() {
        _coins++;
        _uiManager.UpdateCoins(_coins);
        }
    }