using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float RotationSpeed;

    private float _rotationX = 0;
    private float _rotationY = 0;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 회전한 양 만큼 누적시킨다
        _rotationX += mouseX * RotationSpeed * Time.deltaTime;
        _rotationY += -1f * mouseY * RotationSpeed * Time.deltaTime;

        // 회전 각도를 제한한다
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        // 카메라의 회전값을 설정한다
        transform.eulerAngles = new Vector3(_rotationY, _rotationX, 0);
    }
}
