using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardSwipe _CardSwipe;
    public GameManager _GameManager;

    [SerializeField] private Image cardImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameCardText;
    [SerializeField] private Sprite emptyCardsBackground;
    
    [HideInInspector] public CardData currentCard;

    [HideInInspector] public List<int[]> rightDirection = new();
    [HideInInspector] public List<int[]> leftDirection = new();

    public void SizeElement(string device)
    {
        if(device == "desktop") cardImage.rectTransform.localScale = new Vector3(0.3f, 1);
    }

    public void UpdateCard()
    {
        if(currentCard.cardSprite != null) cardImage.sprite = currentCard.cardSprite;
        else cardImage.sprite = emptyCardsBackground;
        
        descriptionText.text = currentCard.description;
        nameCardText.text = currentCard.cardName;
        ResourcesInfo();
        _CardSwipe.NewCard();
    }

    void ResourcesInfo()
    {
        rightDirection.Clear(); leftDirection.Clear();
        RightResourcesInfo(currentCard.rightSide.ReputationEffect, 0);
        RightResourcesInfo(currentCard.rightSide.PeopleEffect, 1);
        RightResourcesInfo(currentCard.rightSide.GunEffect, 2);
        RightResourcesInfo(currentCard.rightSide.FoodEffect, 3);
        LeftResourcesInfo(currentCard.leftSide.ReputationEffect, 0);
        LeftResourcesInfo(currentCard.leftSide.PeopleEffect, 1);
        LeftResourcesInfo(currentCard.leftSide.GunEffect, 2);
        LeftResourcesInfo(currentCard.leftSide.FoodEffect, 3);
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
