using UnityEngine;

public class LayoutSwitcherSimple : MonoBehaviour
{
    [SerializeField] private GameObject portraitLayoutObject;
    [SerializeField] private GameObject landscapeLayoutObject;

    void Update()
    {
        if (Screen.height > Screen.width)
        {
            if (!portraitLayoutObject.activeSelf)
            {
                portraitLayoutObject.SetActive(true);
                landscapeLayoutObject.SetActive(false);
            }
        }
        else
        {
            if (!landscapeLayoutObject.activeSelf)
            {
                portraitLayoutObject.SetActive(false);
                landscapeLayoutObject.SetActive(true);
            }
        }
    }
}
