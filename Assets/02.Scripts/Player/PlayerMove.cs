using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float OriginalSpeed = 10f;
    public float DashSpeed = 15f;
    public float RollSpeed = 30f;
    public float RollTime = 0.5f;
    public float RollStamina = 3f;
    public float JumpPower = 10f;
    public float MaxStamina = 10f;
    public float DashStamina = 1f;
    public float StaminaGainPerSecond = 2.5f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector3 _rollDirection;
    private float _yVelocity = 0f;
    private float _rollTimer = 0f;
    private bool _isJumping = false;
    private bool _isDoubleJumping = false;
    private bool _isUsingStamina = false;
    private bool _isRolling = false;
    private float _speed;
    private float _stamina;

    private const float GRAVITY = -9.8f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _speed = OriginalSpeed;
        _stamina = MaxStamina;
        UI_Canvas.Instance.UpdateStamina(_stamina / MaxStamina);
    }

    private void Update()
    {
        SetDirection();

        Roll();
        if (_isRolling) return;

        Dash();
        Move();
        GainStamina();
    }

    private void Roll()
    {
        if (_characterController.isGrounded && Input.GetButtonDown("Roll") && _isRolling == false)
        {
            if (_stamina < RollStamina) return;

            _stamina -= RollStamina;
            _isRolling = true;
            _speed = RollSpeed;
            _rollDirection = _moveDirection;
            UI_Canvas.Instance.UpdateStamina(_stamina / MaxStamina);
        }

        if (_isRolling == false) return;

        _yVelocity += GRAVITY * Time.deltaTime;
        _rollDirection.y = _yVelocity;
        _rollTimer += Time.deltaTime;
        _characterController.Move(_rollDirection * _speed * Time.deltaTime);

        if (_rollTimer >= RollTime)
        {
            _isRolling = false;
            _rollTimer = 0f;
            _speed = OriginalSpeed;
            _rollDirection = Vector3.back;
        }
    }

    private void SetDirection()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(h, 0, v);
        _moveDirection.Normalize();

        _moveDirection = Camera.main.transform.TransformDirection(_moveDirection);
    }

    private void Move()
    {
        if (_characterController.isGrounded)
        {
            _yVelocity = 0f; // 바닥에 닿았을 때 y축 속도를 초기화합니다.
            _isJumping = false; // 점프 상태 초기화
            _isDoubleJumping = false; // 더블 점프 상태 초기화
        }
        Jump();

        _yVelocity += GRAVITY * Time.deltaTime;
        _moveDirection.y = _yVelocity;

        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isJumping == false)
        {
            _yVelocity = JumpPower;
            _isJumping = true;
        }
        else if (Input.GetButtonDown("Jump") && _isDoubleJumping == false)
        {
            _yVelocity = JumpPower;
            _isDoubleJumping = true;
        }
    }

    private void Dash()
    {
        if (Input.GetButton("Dash") == true)
        {
            _stamina -= DashStamina * Time.deltaTime;
            if (_stamina <= 0)
            {
                _speed = OriginalSpeed;
                _isUsingStamina = false;
                return;
            }
            _speed = DashSpeed;
            _isUsingStamina = true;
            UI_Canvas.Instance.UpdateStamina(_stamina / MaxStamina);
        }
        else
        {
            _speed = OriginalSpeed;
            _isUsingStamina = false;
        }
    }

    private void GainStamina()
    {
        if (_isUsingStamina == true) return;

        _stamina += StaminaGainPerSecond * Time.deltaTime;
        _stamina = Mathf.Clamp(_stamina, 0, MaxStamina);
        UI_Canvas.Instance.UpdateStamina(_stamina / MaxStamina);
    }
}
