using UnityEngine;
using Microlight.MicroBar;

public class UI_PlayerHealthBar : MonoBehaviour
{
    public MicroBar HealthBar => _healthBar;

    private MicroBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<MicroBar>();
        _healthBar.Initialize(0f);
    }
}
