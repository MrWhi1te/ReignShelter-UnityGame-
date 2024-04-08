using System.Collections;
using UnityEngine;
using YG;

public class SaveLoad : MonoBehaviour
{
    public GameManager _GameManager;

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
    }

    public void Save()
    {
        YandexGame.savesData.indexCard = _GameManager.indexCard;
        YandexGame.savesData.dayCount = _GameManager.dayCount;

        for (int i = 0; i < 4; i++) YandexGame.savesData.resources[i] = _GameManager._ResourcesData.resources[i].resourceValue;

        if (!YandexGame.savesData.saver) YandexGame.savesData.saver = true;

        YandexGame.SaveProgress();
    }
}
