using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Canvas : MonoBehaviour
{
    public static UI_Canvas Instance;
    public Slider StaminaSlider;
    public TextMeshProUGUI BombCounter;
    public TextMeshProUGUI BulletCounter;
    public UI_ReloadIndicator ReloadIndicator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ReloadIndicator.gameObject.SetActive(false);
    }

    public void UpdateStamina(float value)
    {
        StaminaSlider.value = value;
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
