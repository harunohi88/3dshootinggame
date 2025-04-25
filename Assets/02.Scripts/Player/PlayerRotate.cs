using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float RotationSpeed;

    private float _rotationX = 0;
    private float _rotationY = 0;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        _rotationX += mouseX * RotationSpeed * Time.deltaTime;

        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        transform.eulerAngles = new Vector3(0, _rotationX, 0);
    }
}
