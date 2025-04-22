using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        transform.position = Target.position;
    }
}
