using UnityEngine;
using UnityEngine.UI;

public class ResourcesData : MonoBehaviour
{
    public GameManager _GameManager;
    public Resource[] resources;

    [SerializeField] private Image[] resourcesInfoImage;
    [SerializeField] private Sprite[] resourcesInfoSprite;

    public void SizeElement(string device)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].SizeElement(device);
        }
    }

    private void Start()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].StartGame();
        }
    }

    public void ResetResources()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].NewGame();
        }
    }

    public void SetResourcesValue(float[] value)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].SetReputationValue(value[i]);
        }
    }

    public void DisplayResourcesInfo(int resource, int value, int spriteNumber)
    {
        if(value == 0)
        {
            resourcesInfoImage[resource].transform.eulerAngles = new Vector3(0, 0, 0);
            resourcesInfoImage[resource].color = Color.green;
        }
        else
        {
            resourcesInfoImage[resource].transform.eulerAngles = new Vector3(0, 0, 180);
            resourcesInfoImage[resource].color = Color.red;
        }
        resourcesInfoImage[resource].sprite = resourcesInfoSprite[spriteNumber];
        resourcesInfoImage[resource].gameObject.SetActive(true);
    }

    public void ClearResourcesInfo()
    {
        for(int i = 0; i < resourcesInfoImage.Length; i++)
        {
            resourcesInfoImage[i].gameObject.SetActive(false);
        }
    }

    public void CheckResource(string direction, int number)
    {
        bool postive = true;
        for(int i = 0; i < resources.Length; i++)
        {
            if(resources[i].resourceValue <= 0.0001)
            {
                postive = false;
                _GameManager.NextCard("die", i);
                break;
            }
        }

        if (postive)
        {
            _GameManager.NextCard(direction, number);
        }
    }
}
