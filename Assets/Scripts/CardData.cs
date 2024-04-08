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
    public SideEffect rightSide;

    [Header("Left")]
    public SideEffect leftSide;
}

[System.Serializable]
public class SideEffect{
    public string SwipeText;
    public float ReputationEffect;
    public float PeopleEffect;
    public float GunEffect;
    public float FoodEffect;
    public string NextCards;
    public int NextCardNumber;
}
