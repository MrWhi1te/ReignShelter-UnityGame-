using UnityEngine;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    public ResourcesData RD;
    public CardManager CM;

    [SerializeField] private Text textDirection;
    [SerializeField] private Image imageDirection;

    [SerializeField] private float speed;
    [SerializeField] private float rotationCoefficient; 
    [SerializeField] private float sideTrigger;
    [SerializeField] private float divide;
    [SerializeField] private float speedRotation;

    private Transform thisTransf;
    private Color textDirectionColor;
    private Color imageDirectionColor;
    private Vector3 startPosition; 
    private bool isDragging = false; 
    private bool isSwipeHandled = false;

    private Vector3 startRotation = new Vector3(0, 0, 0);
    private Vector3 NewCardRotation = new Vector3(0, 180, 0);
    private bool isRotation = false;

    private void Start()
    {
        thisTransf = transform;
        startPosition = thisTransf.position;
        textDirectionColor = textDirection.color;
        imageDirectionColor = imageDirection.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) isDragging = true;

        if (Input.GetMouseButtonUp(0)) isDragging = false;

        if (isDragging)
        {
            float dragDirection = Input.GetAxis("Mouse X");
            thisTransf.Translate(dragDirection * speed * Time.deltaTime * Vector3.right);

            thisTransf.eulerAngles = new Vector3(0, 0, thisTransf.position.x * rotationCoefficient);

            if (thisTransf.position.x > sideTrigger)
            {
                textDirection.alignment = TextAnchor.MiddleLeft;
                textDirection.text = CM.currentCard.rSwipeText;
                CheckResources("Right");
            }
            else if (thisTransf.position.x < -sideTrigger)
            {
                textDirection.alignment = TextAnchor.MiddleRight;
                textDirection.text = CM.currentCard.lSwipeText;
                CheckResources("Left");
            }
            else
            {
                textDirection.text = "";
                RD.ClearResourcesInfo();
                isSwipeHandled = false;
            }

            UpdateColorDirection();
        }
        else
        {
            RD.ClearResourcesInfo();
            if (thisTransf.position.x > sideTrigger && !isSwipeHandled)
            {
                CheckDirection("Right");
                isSwipeHandled = true;
            }
            else if(thisTransf.position.x < -sideTrigger && !isSwipeHandled)
            {
                CheckDirection("Left");
                isSwipeHandled = true;
            }
        }

        if (isRotation)
        {
            thisTransf.eulerAngles = Vector3.MoveTowards(thisTransf.eulerAngles, startRotation, speedRotation);
        }
        if (isRotation && thisTransf.eulerAngles == startRotation) isRotation = false;
    }

    public void NewCard()
    {
        isRotation = true;
        thisTransf.SetPositionAndRotation(startPosition, Quaternion.Euler(0, 0, 0));
        thisTransf.eulerAngles = NewCardRotation;
        UpdateColorDirection();
    }

    private void UpdateColorDirection()
    {
        textDirectionColor.a = Mathf.Min((Mathf.Abs(thisTransf.position.x) - sideTrigger) / divide, 1);
        textDirection.color = textDirectionColor;

        imageDirectionColor.a = Mathf.Min((Mathf.Abs(thisTransf.position.x) - sideTrigger) / divide, 1);
        imageDirection.color = imageDirectionColor;
    }

    private void CheckDirection(string direction)
    {
        RD.ClearResourcesInfo(); 
        if (direction == "Right")
        {
            RD.SetResourcesValue(new float[] { CM.currentCard.rReputationEffect, CM.currentCard.rPeopleEffect, CM.currentCard.rGunEffect, CM.currentCard.rFoodEffect });
            if (CM.currentCard.adsCard)
            {
                // ADS
            }
            else if (CM.currentCard.endCard)
            {

            }
            else RD.CheckResource(CM.currentCard.rNextCards, CM.currentCard.rNextCardNumber);
        }
        else if(direction == "Left")
        {
            RD.SetResourcesValue(new float[] { CM.currentCard.lReputationEffect, CM.currentCard.lPeopleEffect, CM.currentCard.lGunEffect, CM.currentCard.lFoodEffect });
            if (CM.currentCard.endCard)
            {

            }
            else RD.CheckResource(CM.currentCard.lNextCards, CM.currentCard.lNextCardNumber);
        }
    }

    private void CheckResources(string direction)
    {
        RD.ClearResourcesInfo();
        if(direction == "Right")
        {
            for(int i = 0; i < CM.rightDirection.Count; i++) RD.DisplayResourcesInfo(CM.rightDirection[i][0], CM.rightDirection[i][1], CM.rightDirection[i][2]);
        }
        else
        {
            for (int i = 0; i < CM.leftDirection.Count; i++) RD.DisplayResourcesInfo(CM.leftDirection[i][0], CM.leftDirection[i][1], CM.leftDirection[i][2]);
        }
    }
}
