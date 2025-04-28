using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    public float DisplayTime = 1f;
    public float FadeTime = 2f;

    private Image _bloodScreen;

    private void Start()
    {
        _bloodScreen = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void ShowHitEffect(float healthRate)
    {
        StopAllCoroutines();
        _bloodScreen.color = new Color(1, 1, 1, 1 - healthRate);
        gameObject.SetActive(true);

        StartCoroutine(FadeOut_Coroutine());
    }

    private IEnumerator FadeOut_Coroutine()
    {
        Color originalColor = _bloodScreen.color;
        float timer = 0f;

        while (timer < DisplayTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < FadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0, timer / FadeTime);
            _bloodScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        _bloodScreen.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
