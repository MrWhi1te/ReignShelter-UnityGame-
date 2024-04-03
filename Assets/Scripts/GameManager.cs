using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public DayCount _DayCount;
    public CardManager _CardManager;
    public ResourcesData _ResourcesData;
    public Sounds _Sounds;

    [HideInInspector] public int indexCard = 0;
    [HideInInspector] public int dayCount;
    [HideInInspector] public bool trainer;

    [SerializeField] private Text dayCountText;
    [SerializeField] private GameObject cardStartObj;
    [SerializeField] private GameObject startGamePan;

    [Header("Cards")]
    [SerializeField] private CardData[] mainCards;
    [SerializeField] private CardData[] randomCards;
    [SerializeField] private CardData[] eventCards;
    [SerializeField] private CardData[] trainerCards;
    [SerializeField] private CardData[] dieCards;
    [SerializeField] private CardData adsCard;

    public Dictionary<string, CardData[]> dictionaryCards;

    private int randomEventsCardsCount;
    private int adsCardsCount;

    System.Random rand = new System.Random();


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void StartGame()
    {
        if (YandexGame.EnvironmentData.deviceType == "desktop")
        {
            cardStartObj.transform.localScale = new Vector3(0.3f, 1);
            _CardManager.SizeElement("desktop");
            _ResourcesData.SizeElement("desktop");
        }
        dictionaryCards = new Dictionary<string, CardData[]>()
        {
            { "random", randomCards },
            { "event", eventCards },
            { "trainer", trainerCards },
            { "die", dieCards }
        };
        
        if(indexCard == 0)
        {
            ReshuffleCards();
            dictionaryCards.Add( "", mainCards);
        }
        else dictionaryCards.Add("", mainCards);

        dayCountText.text = dayCount + " дней выживания";
        startGamePan.SetActive(false);
        ActiveCardStart();
        if(!trainer) _CardManager.currentCard = dictionaryCards["trainer"][0];
        else _CardManager.currentCard = dictionaryCards[""][indexCard];
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
        _Sounds.PlayEnterCards();
        cardStartObj.SetActive(false); cardStartObj.SetActive(true);
    }

    public void NextCard(string direction, int number)
    {
        if (direction != "")
        {
            _CardManager.currentCard = dictionaryCards[direction][number];
            if (direction == "die") _Sounds.PlayAudioDie();
        }
        else if (randomEventsCardsCount >= 3)
        {
            int r = rand.Next(0, dictionaryCards["random"].Length);
            _CardManager.currentCard = dictionaryCards["random"][r];
            randomEventsCardsCount = 0;
        }
        else if (adsCardsCount >= 7)
        {
            _CardManager.currentCard = adsCard;
            adsCardsCount = 0;
        }
        else
        {
            indexCard++;
            dayCount++;
            if (indexCard < mainCards.Length) _CardManager.currentCard = dictionaryCards[""][indexCard];
            else
            {
                indexCard = 0;
                Debug.Log("WIN!");
            }
            randomEventsCardsCount++;
            adsCardsCount++;
        }
        dayCountText.text = dayCount + " дней выживания";
        _Sounds.PlayAudioSwipe();
        _CardManager.UpdateCard();
    }

    public void ReshuffleCards()
    {
        for(int i = 1; i < mainCards.Length; i++)
        {
            int a = rand.Next(i, mainCards.Length);
            CardData card = mainCards[i];
            mainCards[i] = mainCards[a];
            mainCards[a] = card;
        }
    }


    //ADS

    private void Rewarded(int id)
    {
        if (id == 0)
        {
            _ResourcesData.SetResourcesValue(new float[] { 0.3f, 0.3f, 0.3f, 0.3f });
            NextCard("",0);
        }
    }

    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
