using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public float resourceValue;
    public Image resourceImage;
    public Image resourceParentImage;
    public int number;

    public void SizeElement(string device)
    {
        if (device == "desktop") resourceParentImage.rectTransform.localScale = new Vector3(0.6f, 1);
    }

    public void StartGame()
    {
        resourceImage.fillAmount = resourceValue;
    }

    public void NewGame()
    {
        resourceValue = 0.4f;
        StartGame();
    }

    private void UpdateValue(float value)
    {
        resourceValue += value;

        if (resourceValue > 1)
        {
            resourceValue = 1;
        }
        else if (resourceValue <= 0.001)
        {
            resourceValue = 0;
        }
        resourceImage.fillAmount = resourceValue;
        StartCoroutine(ColorValue(value));
    }

    IEnumerator ColorValue(float value)
    {
        if (value > 0) resourceImage.color = Color.green;
        else if (value < 0) resourceImage.color = Color.red;
        yield return new WaitForSeconds(1);
        resourceImage.color = Color.white;
        yield break;
    }

    public void SetReputationValue(float value)
    {
        if (value != 0) UpdateValue(value);
    }
}
