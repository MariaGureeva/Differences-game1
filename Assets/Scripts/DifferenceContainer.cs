using UnityEngine;
using UnityEngine.UI;

public class DifferenceContainer : MonoBehaviour
{
    [SerializeField] private GameObject correctVersionItem;
    [SerializeField] private GameObject successIndicatorPrefab;
    [SerializeField] private GameObject successEffectPrefab;
    [SerializeField] private Transform fxContainer;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnDifferenceFound);
    }

    private void OnDifferenceFound()
    {
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
        gameObject.SetActive(false);
    }
}