using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStats PlayerStats;

    private Animator _animator;
    [SerializeField] private int _currentHealth;


    private void Start()
    {
        _currentHealth = PlayerStats.MaxHealth;
        _animator = GetComponentInChildren<Animator>();
        UI_HUD.Instance.UpdatePlayerHealthBar(_currentHealth, PlayerStats.MaxHealth);
    }

    public void TakeDamage(Damage damage)
    {
        _currentHealth -= damage.Value;

        UI_Effects.Instance.BloodScreen.ShowHitEffect((float)_currentHealth / PlayerStats.MaxHealth);
        UI_HUD.Instance.UpdatePlayerHealthBar(_currentHealth, PlayerStats.MaxHealth);
        _animator.SetLayerWeight(1, 1 - ((float)_currentHealth / PlayerStats.MaxHealth));

        if (_currentHealth <= 0)
        {
            // game over logic
            Debug.Log("Player is dead");
        }
    }
}
