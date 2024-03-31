using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardSwipe _CardSwipe;
    public GameManager _GameManager;

    public List<CardData> cards;

    [SerializeField] private Image cardImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameCardText;
    
    [HideInInspector] public CardData currentCard;

    [HideInInspector] public List<int[]> rightDirection = new();
    [HideInInspector] public List<int[]> leftDirection = new();

    private int randomEventsCardsCount;
    private int adsCardsCount;

    private void Start()
    {
        currentCard = cards[_GameManager.indexCard];
        UpdateCard();
    }

    public void UpdateCard()
    {
        currentCard = cards[_GameManager.indexCard];
        cardImage.sprite = currentCard.cardSprite;
        descriptionText.text = currentCard.description;
        nameCardText.text = currentCard.cardName;
        _GameManager.dayCountText.text = _GameManager.dayCount + " дней выживания";
        ResourcesInfo();
        _CardSwipe.NewCard();
    }

    public void NextCard(string direction, int number)
    {
        if(direction != "")
        {
            //cards[number];
        }
        else if (randomEventsCardsCount >= 3)
        {
            randomEventsCardsCount = 0;
        }
        else if (adsCardsCount >= 7)
        {
            adsCardsCount = 0;
        }
        else
        {
            _GameManager.indexCard++;
            _GameManager.dayCount += UnityEngine.Random.Range(1, 2);
            if (_GameManager.indexCard < cards.Count) UpdateCard();
            else
            {
                _GameManager.indexCard = 0;
                _GameManager.ActiveCardStart();
                UpdateCard();
            }
            randomEventsCardsCount++;
            adsCardsCount++;
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
}
