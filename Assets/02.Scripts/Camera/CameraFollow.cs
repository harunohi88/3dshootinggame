using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        transform.position = Target.position;
    }
}
