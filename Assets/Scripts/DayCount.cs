using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayCount : MonoBehaviour
{
    public GameManager _GameManager;

    public Text dayCountText;
    public float resetTime = 4f;
    public GameObject startGameBttn;

    private int dayCount;

    private void Start()
    {
        dayCountText.text = _GameManager.dayCount + " дней выживания";
    }

    public void ResetCounter()
    {
        dayCount = _GameManager.dayCount;
        startGameBttn.SetActive(false);
        dayCountText.text = dayCount + " дней выживания";
        StartCoroutine(ResetCounterCoroutine());
    }

    private IEnumerator ResetCounterCoroutine()
    {
        float startTime = Time.time; // Запоминаем время начала сброса
        int initialDayCount = dayCount; // Запоминаем начальное значение счетчика

        while (dayCount > 0)
        {
            // Вычисляем процент времени, прошедшего с начала сброса
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / resetTime;

            // Вычисляем новое значение счетчика на основе прогресса
            dayCount = Mathf.RoundToInt(Mathf.Lerp(initialDayCount, 0, progress));

            dayCountText.text = dayCount + " дней выживания";

            yield return null;
        }

        // Сбрасываем счетчик до 0, если он не достиг 0 ранее
        _GameManager.dayCount = 0;
        dayCountText.text = _GameManager.dayCount + " дней выживания";

        startGameBttn.SetActive(true);
    }
}
