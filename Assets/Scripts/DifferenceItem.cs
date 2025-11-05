using UnityEngine;
using UnityEngine.UI;

public class DifferenceItem : MonoBehaviour
{
    [SerializeField] private GameObject correctVersionItem;
    [SerializeField] private GameObject successIndicatorPrefab;
    [SerializeField] private GameObject successEffectPrefab;
    [SerializeField] private Transform fxContainer;

    private Button button;
    private Image thisImage; 

    void Awake()
    {
        button = GetComponent<Button>();
        thisImage = GetComponent<Image>();
        button.onClick.AddListener(OnDifferenceFound);
    }

    private void OnDifferenceFound()
    {
        if (correctVersionItem != null) 
        {
            Image correctImage = correctVersionItem.GetComponent<Image>();
            if (correctImage != null) 
            {
                Sprite correctSprite = correctImage.sprite;
                if (correctSprite != null)
                {
                    thisImage.sprite = correctSprite;
                }
            }
        }
        if (successIndicatorPrefab != null && correctVersionItem != null)
        {
            Instantiate(successIndicatorPrefab, correctVersionItem.transform.position, Quaternion.identity, correctVersionItem.transform.parent);
        }
        
        if (successEffectPrefab != null && fxContainer != null)
        {
            GameObject effect = Instantiate(successEffectPrefab, fxContainer);
            effect.transform.position = transform.position;
        }
        
        GameManager.instance.ReportDifferenceFound();
        button.onClick.RemoveListener(OnDifferenceFound);
    }
}