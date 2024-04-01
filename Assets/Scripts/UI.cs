using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameManager _GameManager;
    public CardManager _CardManager;

    [Header("Mobile")]
    [SerializeField] private Text dayCountText;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameCardText;
    [SerializeField] private Image resourceImage;

    [SerializeField] private Sprite emptyCardsBackground;

    public void DayCountUI()
    {
        dayCountText.text = _GameManager.dayCount + " дней выживания";
    }

    public void CurrentCardUI()
    {
        if (_CardManager.currentCard.cardSprite != null) cardImage.sprite = _CardManager.currentCard.cardSprite;
        else cardImage.sprite = emptyCardsBackground;

        descriptionText.text = _CardManager.currentCard.description;
        nameCardText.text = _CardManager.currentCard.cardName;
    }
}
