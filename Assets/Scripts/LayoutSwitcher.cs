using UnityEngine;
using System.Collections.Generic;

public class LayoutSwitcher : MonoBehaviour
{
    [SerializeField] private Transform horizontalParent;
    [SerializeField] private Transform verticalParent;
    [SerializeField] private List<RectTransform> cardsToManage;

    private bool isCurrentlyVertical = true;

        void Start()
        {
            CheckOrientationAndReparent(true);
        }

        void Update()
        {
            CheckOrientationAndReparent(false);
        }

        private void CheckOrientationAndReparent(bool forceCheck)
        {
            bool isVerticalNow = Screen.height > Screen.width;

            if (forceCheck || isVerticalNow != isCurrentlyVertical)
            {
                Transform targetParent = isVerticalNow ? verticalParent : horizontalParent;

                foreach (RectTransform card in cardsToManage)
                {
                    card.SetParent(targetParent, false);
                }
                isCurrentlyVertical = isVerticalNow;
            }
        }
    }