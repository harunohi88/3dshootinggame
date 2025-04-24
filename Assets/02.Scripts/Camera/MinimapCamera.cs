using JetBrains.Annotations;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform Target;
    public float CameraHeight = 10f;

    public void LateUpdate()
    {
        Vector3 newPosition = Target.position;
        newPosition.y = CameraHeight; // 카메라 높이 설정

        transform.position = newPosition; // 카메라 위치 업데이트
    }
}
