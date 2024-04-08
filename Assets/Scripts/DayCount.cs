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
        dayCountText.text = _GameManager.dayCount + " ���� ���������";
    }

    public void ResetCounter()
    {
        dayCount = _GameManager.dayCount;
        startGameBttn.SetActive(false);
        dayCountText.text = dayCount + " ���� ���������";
        StartCoroutine(ResetCounterCoroutine());
    }

    private IEnumerator ResetCounterCoroutine()
    {
        float startTime = Time.time; // ���������� ����� ������ ������
        int initialDayCount = dayCount; // ���������� ��������� �������� ��������

        while (dayCount > 0)
        {
            // ��������� ������� �������, ���������� � ������ ������
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / resetTime;

            // ��������� ����� �������� �������� �� ������ ���������
            dayCount = Mathf.RoundToInt(Mathf.Lerp(initialDayCount, 0, progress));

            dayCountText.text = dayCount + " ���� ���������";

            yield return null;
        }

        // ���������� ������� �� 0, ���� �� �� ������ 0 �����
        _GameManager.dayCount = 0;
        dayCountText.text = _GameManager.dayCount + " ���� ���������";

        startGameBttn.SetActive(true);
    }
}
