using UnityEngine;
using UnityEngine.UI;

public class UI_Canvas : MonoBehaviour
{
    public static UI_Canvas Instance;
    public Slider StaminaSlider;

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

    public void UpdateStamina(float value)
    {
        StaminaSlider.value = value;
    }
}
