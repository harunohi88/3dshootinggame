using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : Singleton<UI_HUD>
{
    public Slider StaminaSlider;
    public TextMeshProUGUI BombCounter;
    public TextMeshProUGUI BulletCounter;
    public UI_ReloadIndicator ReloadIndicator;
    public UI_PlayerHealthBar PlayerHealthBar;

    private void Start()
    {
        ReloadIndicator.gameObject.SetActive(false);
    }

    public void UpdateStamina(float value)
    {
        StaminaSlider.value = value;
    }

    public void UpdatePlayerHealthBar(float currentHealth, float newMaxHealth)
    {
        PlayerHealthBar.HealthBar.SetNewMaxHP(newMaxHealth);
        PlayerHealthBar.HealthBar.UpdateBar(currentHealth);
    }

    public void UpdateBombCounter(int remain, int max)
    {
        if (remain == 0)
        {
            BombCounter.color = Color.red;
        }
        else
        {
            BombCounter.color = Color.white;
        }
        BombCounter.text = $"폭탄: {remain} / {max}";
    }

    public void UpdateBulletCounter(int remain, int max)
    {
        if (remain == 0)
        {
            BulletCounter.color = Color.red;
        }
        else
        {
            BulletCounter.color = Color.white;
        }
        BulletCounter.text = $"총알: {remain} / {max}";
    }

    public void UpdateReloadIndicator(float value)
    {
        ReloadIndicator.ProgressSlider.value = value;
    }
}
