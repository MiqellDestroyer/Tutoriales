using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Selection
{
    public class SelectionController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        public static List<SelectableUnit> selectedUnits;

        [SerializeField]
        private Image selectionBoxImage;
        public static Rect selectionBoxRect;
        private Vector2 selectionStartPos;

        public delegate void SelectionAction();
        public static event SelectionAction OnBoxSelection;

        private void Awake()
        {
            selectedUnits = new List<SelectableUnit>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            selectionBoxImage.gameObject.SetActive(true);
            selectionBoxRect = new Rect();
            selectionStartPos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.position.x < selectionStartPos.x)
            {
                selectionBoxRect.xMin = eventData.position.x;
                selectionBoxRect.xMax = selectionStartPos.x;
            }
            else
            {
                selectionBoxRect.xMin = selectionStartPos.x;
                selectionBoxRect.xMax = eventData.position.x;
            }
            if (eventData.position.y < selectionStartPos.y)
            {
                selectionBoxRect.yMin = eventData.position.y;
                selectionBoxRect.yMax = selectionStartPos.y;
            }
            else
            {
                selectionBoxRect.yMin = selectionStartPos.y;
                selectionBoxRect.yMax = eventData.position.y;
            }

            selectionBoxImage.rectTransform.offsetMin = selectionBoxRect.min;
            selectionBoxImage.rectTransform.offsetMax = selectionBoxRect.max;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            selectionBoxImage.gameObject.SetActive(false);

            if (OnBoxSelection != null)
            {
                OnBoxSelection();
            }
            foreach (SelectableUnit unit in selectedUnits)
            {
                unit.SelectUnit();
            }

        }
    }
}
