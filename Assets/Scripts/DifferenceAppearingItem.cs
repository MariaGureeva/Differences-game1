using UnityEngine;
using UnityEngine.UI;

public class DifferenceAppearingItem : MonoBehaviour
{
    [SerializeField] private GameObject itemToAppear;
    [SerializeField] private GameObject checkmarkTarget;
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
        if (itemToAppear != null)
        {
            itemToAppear.SetActive(true);
        }
        if (successIndicatorPrefab != null && checkmarkTarget != null)
        {
            Instantiate(successIndicatorPrefab, checkmarkTarget.transform.position, Quaternion.identity, checkmarkTarget.transform.parent);
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