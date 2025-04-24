using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform Target;
    public float CameraHeight = 10f;
    public float CameraZoomRate;
    public float CameraProjecdtionMax;
    public float CameraProjecdtionMin;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = Target.position;
        newPosition.y = CameraHeight; // 카메라 높이 설정

        Vector3 newEulerAngle = Target.eulerAngles;
        newEulerAngle.x = 90f; // 카메라 회전 설정

        newEulerAngle.z = 0f;

        transform.position = newPosition; // 카메라 위치 업데이트
        transform.eulerAngles = newEulerAngle; // 카메라 회전 업데이트
    }

    public void ZoomInCamera()
    {
        _camera.orthographicSize -= CameraZoomRate;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, CameraProjecdtionMin, CameraProjecdtionMax);
    }

    public void ZoomOutCamera()
    {
        _camera.orthographicSize += CameraZoomRate;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, CameraProjecdtionMin, CameraProjecdtionMax);
    }
}
