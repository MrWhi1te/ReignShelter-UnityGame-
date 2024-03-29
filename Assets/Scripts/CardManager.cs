using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardSwipe _CardSwipe;
    public DayCount _DayCount;

    public int indexCard = 0;
    [HideInInspector] public int dayCount;

    public List<CardData> cards;

    [SerializeField] private Image cardImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameCardText;
    [SerializeField] private Text dayCountText;
    [SerializeField] private GameObject cardStartObj;
    [SerializeField] private GameObject startGamePan;

    [HideInInspector] public CardData currentCard;

    [HideInInspector] public List<int[]> rightDirection = new();
    [HideInInspector] public List<int[]> leftDirection = new();

    private void Start()
    {
        currentCard = cards[indexCard];
        UpdateCard();
    }

    private void UpdateCard()
    {
        currentCard = cards[indexCard];
        cardImage.sprite = currentCard.cardSprite;
        descriptionText.text = currentCard.description;
        nameCardText.text = currentCard.cardName;
        dayCountText.text = dayCount + " дней выживания";
        ResourcesInfo();
        _CardSwipe.NewCard();
    }

    public void NextCard(string direction, int number)
    {
        if(direction != "")
        {

        }
        else
        {
            indexCard++;
            dayCount += UnityEngine.Random.Range(1, 6);
            if (indexCard < cards.Count) UpdateCard();
            else
            {
                indexCard = 0;
                cardStartObj.SetActive(false); cardStartObj.SetActive(true);
                UpdateCard();
            }
        }
    }

    void ResourcesInfo()
    {
        rightDirection.Clear(); leftDirection.Clear();
        RightResourcesInfo(currentCard.rReputationEffect, 0);
        RightResourcesInfo(currentCard.rPeopleEffect, 1);
        RightResourcesInfo(currentCard.rGunEffect, 2);
        RightResourcesInfo(currentCard.rFoodEffect, 3);
        LeftResourcesInfo(currentCard.lReputationEffect, 0);
        LeftResourcesInfo(currentCard.lPeopleEffect, 1);
        LeftResourcesInfo(currentCard.lGunEffect, 2);
        LeftResourcesInfo(currentCard.lFoodEffect, 3);
    }

    void RightResourcesInfo(float effect, int resourceIndex)
    {
        if(effect != 0)
        {
            int[] array = new int[3];
            array[0] = resourceIndex;
            array[1] = effect > 0 ? 0 : 1;
            array[2] = Math.Abs(effect) > 0.06 ? 1 : 0;
            rightDirection.Add(array);
        }
    }

    void LeftResourcesInfo(float effect, int resourceIndex)
    {
        if (effect != 0)
        {
            int[] array = new int[3];
            array[0] = resourceIndex;
            array[1] = effect > 0 ? 0 : 1;
            array[2] = Math.Abs(effect) > 0.06 ? 1 : 0;
            leftDirection.Add(array);
        }
    }

    public void StartGame()
    {
        startGamePan.SetActive(false);
        cardStartObj.SetActive(false); cardStartObj.SetActive(true);
        UpdateCard();
    }

    public void ResetGame()
    {
        indexCard = 0;
        startGamePan.SetActive(true);
        _DayCount.ResetCounter();
    }
}
