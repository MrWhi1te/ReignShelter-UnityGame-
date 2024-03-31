using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DayCount _DayCount;
    public CardManager _CardManager;

    [HideInInspector] public int indexCard = 0;
    [HideInInspector] public int dayCount;
   
    public Text dayCountText;
    [SerializeField] private GameObject cardStartObj;
    [SerializeField] private GameObject startGamePan;

    public void StartGame()
    {
        startGamePan.SetActive(false);
        ActiveCardStart();
        _CardManager.UpdateCard();
    }

    public void ResetGame()
    {
        indexCard = 0;
        startGamePan.SetActive(true);
        _DayCount.ResetCounter();
    }

    public void ActiveCardStart()
    {
        cardStartObj.SetActive(false); cardStartObj.SetActive(true);
    }
}
