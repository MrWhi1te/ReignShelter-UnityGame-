using System.Collections;
using UnityEngine;
using YG;

public class SaveLoad : MonoBehaviour
{
    public GameManager _GameManager;

    [SerializeField] private UnityEngine.UI.Image loadImage;
    [SerializeField] private GameObject loadPan;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void GetLoad()
    {
        if (YandexGame.savesData.saver)
        {
            _GameManager.indexCard = YandexGame.savesData.indexCard;
            _GameManager.dayCount = YandexGame.savesData.dayCount;

            for(int i=0; i < 4; i++) _GameManager._ResourcesData.resources[i].resourceValue = YandexGame.savesData.resources[i];
        }
        StartCoroutine(LoadGame());
    }

    public void Save()
    {
        YandexGame.savesData.indexCard = _GameManager.indexCard;
        YandexGame.savesData.dayCount = _GameManager.dayCount;

        for (int i = 0; i < 4; i++) YandexGame.savesData.resources[i] = _GameManager._ResourcesData.resources[i].resourceValue;

        if (!YandexGame.savesData.saver) YandexGame.savesData.saver = true;

        YandexGame.SaveProgress();
    }


    IEnumerator LoadGame()
    {
        float fill = 0;
        while (fill < 0.95)
        {
            loadImage.fillAmount = fill;
            fill += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        loadPan.SetActive(false);
        yield break;
    }
}
