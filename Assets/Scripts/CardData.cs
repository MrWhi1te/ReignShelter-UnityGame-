using UnityEngine;

[CreateAssetMenu(fileName = "Card" , menuName = "Cards/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    [TextArea] public string description;
    public bool adsCard;
    public bool endCard;

    [Header("Right")]
    public string rSwipeText;
    public float rReputationEffect;
    public float rPeopleEffect;
    public float rGunEffect;
    public float rFoodEffect;
    public string rNextCards;
    public int rNextCardNumber;

    [Header("Left")]
    public string lSwipeText;
    public float lReputationEffect;
    public float lPeopleEffect;
    public float lGunEffect;
    public float lFoodEffect;
    public string lNextCards;
    public int lNextCardNumber;
}
