using UnityEngine;

public class ButtonGlow : MonoBehaviour
{
    [SerializeField] private GameObject glowObject;
    public void ShowGlow()
    {
        if (glowObject != null)
        {
            glowObject.SetActive(true);
        }
    }

    public void HideGlow()
    {
        if (glowObject != null)
        {
            glowObject.SetActive(false);
        }
    }
}