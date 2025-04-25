using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStats PlayerStats;

    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = PlayerStats.MaxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        _currentHealth -= damage.Value;

        if (_currentHealth <= 0)
        {
            // game over logic
            Debug.Log("Player is dead");
        }
    }
}
