using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public PlayerData PlayerData;
    public LayerMask WallLayer;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector3 _rollDirection;
    private float _yVelocity = 0f;
    private float _rollTimer = 0f;
    private bool _isJumping = false;
    private bool _isDoubleJumping = false;
    private bool _isUsingStamina = false;
    private bool _isRolling = false;
    private bool _isClimbing = false;
    private float _speed;
    private float _stamina;
    private RaycastHit _wallHit;

    private const float GRAVITY = -9.8f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _speed = PlayerData.OriginalSpeed;
        _stamina = PlayerData.MaxStamina;
        UI_Canvas.Instance.UpdateStamina(_stamina / PlayerData.MaxStamina);
    }

    private void Update()
    {
        SetDirection();

        CheckClimbStart();
        if (_isClimbing)
        {
            Climb();
            return;
        }

        Roll();
        if (_isRolling) return;

        Dash();

        Move();

        GainStamina();
    }

    private bool IsWallInFront()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.forward;

        return Physics.Raycast(origin, direction, out _wallHit, PlayerData.WallCheckDistance, WallLayer);
    }

    private void CheckClimbStart()
    {
        if (_isClimbing) return;

        if (IsWallInFront() && Input.GetButton("Climb") && _characterController.isGrounded)
        {
            _isClimbing = true;
            _isUsingStamina = true;
        }
    }

    private void Climb()
    {
        if (!_isClimbing) return;

        if (!IsWallInFront() || !Input.GetButton("Climb") || _stamina <= 0f)
        {
            _isClimbing = false;
            _isUsingStamina = false;
            _yVelocity = 0f;
            return;
        }

        // 벽에 평행한 방향 (카메라 방향에서 벽 법선 성분 제거)
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 climbAlongWall = Vector3.ProjectOnPlane(camForward, _wallHit.normal).normalized;

        // 수직 + 벽 방향 이동
        Vector3 climbDir = Vector3.up + climbAlongWall;
        climbDir.Normalize();

        Vector3 move = climbDir * PlayerData.ClimbSpeed;

        _stamina -= PlayerData.ClimbStamina * Time.deltaTime;
        UI_Canvas.Instance.UpdateStamina(_stamina / PlayerData.MaxStamina);

        _characterController.Move(move * Time.deltaTime);
    }


    private void Roll()
    {
        if (_characterController.isGrounded && Input.GetButtonDown("Roll") && _isRolling == false)
        {
            if (_stamina < PlayerData.RollStamina) return;

            _stamina -= PlayerData.RollStamina;
            _isRolling = true;
            _speed = PlayerData.RollSpeed;
            _rollDirection = _moveDirection;
            UI_Canvas.Instance.UpdateStamina(_stamina / PlayerData.MaxStamina);
        }

        if (_isRolling == false) return;

        _yVelocity += GRAVITY * Time.deltaTime;
        _rollDirection.y = _yVelocity;
        _rollTimer += Time.deltaTime;
        _characterController.Move(_rollDirection * _speed * Time.deltaTime);

        if (_rollTimer >= PlayerData.RollTime)
        {
            _isRolling = false;
            _rollTimer = 0f;
            _speed = PlayerData.OriginalSpeed;
            _rollDirection = Vector3.zero;
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
            _yVelocity = PlayerData.JumpPower;
            _isJumping = true;
        }
        else if (Input.GetButtonDown("Jump") && _isDoubleJumping == false)
        {
            _yVelocity = PlayerData.JumpPower;
            _isDoubleJumping = true;
        }
    }

    private void Dash()
    {
        if (Input.GetButton("Dash") == true)
        {
            _stamina -= PlayerData.DashStamina * Time.deltaTime;
            if (_stamina <= 0)
            {
                _speed = PlayerData.OriginalSpeed;
                _isUsingStamina = false;
                return;
            }
            _speed = PlayerData.DashSpeed;
            _isUsingStamina = true;
            UI_Canvas.Instance.UpdateStamina(_stamina / PlayerData.MaxStamina);
        }
        else
        {
            _speed = PlayerData.OriginalSpeed;
            _isUsingStamina = false;
        }
    }

    private void GainStamina()
    {
        if (_isUsingStamina == true) return;

        _stamina += PlayerData.StaminaGainPerSecond * Time.deltaTime;
        _stamina = Mathf.Clamp(_stamina, 0, PlayerData.MaxStamina);
        UI_Canvas.Instance.UpdateStamina(_stamina / PlayerData.MaxStamina);
    }
}
