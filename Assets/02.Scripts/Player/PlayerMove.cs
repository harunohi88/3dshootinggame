using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpPower = 10f;

    private CharacterController _characterController;
    private const float GRAVITY = -9.8f;
    private float _yVelocity = 0f;
    private bool _isJumping = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);
        direction.Normalize(); // 방향 벡터를 정규화하여 크기를 1로 만듭니다.
        // 메인 카메라를 기준으로 방향을 계산
        direction = Camera.main.transform.TransformDirection(direction);

        if (_characterController.isGrounded)
        {
            _yVelocity = 0f; // 바닥에 닿았을 때 y축 속도를 초기화합니다.
            _isJumping = false; // 점프 상태 초기화
        }
        Jump();

        _yVelocity += GRAVITY * Time.deltaTime;
        direction.y = _yVelocity;

        _characterController.Move(direction * Speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isJumping == false)
        {
            _yVelocity = JumpPower;
            _isJumping = true;
        }
    }
}
