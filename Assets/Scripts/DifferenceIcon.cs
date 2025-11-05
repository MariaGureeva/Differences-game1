using UnityEngine;
using UnityEngine.UI;

public class DifferenceIcon : MonoBehaviour
{
    [SerializeField] private Sprite foundSprite;
    
    private Image iconImage;
    private int thisIconIndex;

    void Awake()
    {
        iconImage = GetComponent<Image>();
        thisIconIndex = transform.GetSiblingIndex(); 
    }

    private void OnEnable()
    {
        GameManager.OnDifferenceWasFound += CheckIfNeedToActivate;
    }

    private void OnDisable()
    {
        GameManager.OnDifferenceWasFound -= CheckIfNeedToActivate;
    }
    
   
    private void CheckIfNeedToActivate(int newTotalFound)
    {
        if (thisIconIndex == newTotalFound - 1)
        {
            iconImage.sprite = foundSprite;
        }
    }
}