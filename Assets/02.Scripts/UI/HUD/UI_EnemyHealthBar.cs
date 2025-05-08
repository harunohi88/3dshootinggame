using UnityEngine;
using Microlight.MicroBar;

public class UI_EnemyHealthBar : MonoBehaviour
{
    public Vector3 Offset = Vector3.up * 2f;

    private MicroBar HealthBar;
    private Camera _mainCamera;

    private void Awake()
    {
        HealthBar = GetComponentInChildren<MicroBar>();
        HealthBar.Initialize(0f);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void LateUpdate()
    {
        Vector3 direction = transform.position - _mainCamera.transform.position;
        direction.y = 0f;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdateHealth(float currentHealth, float maxHelath)
    {
        // 체력바 업데이트
        HealthBar.SetNewMaxHP(maxHelath);
        HealthBar.UpdateBar(currentHealth);
    }

    public void Release()
    {
        HealthBar.Initialize(0f);
        gameObject.SetActive(false);
    }
}
