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

        private void Update()
        {
            SimpleSelection();
        }
        private void SimpleSelection()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    Deselect();
                }
                MakeSimpleSelection();
            }
        }

        private void MakeSimpleSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            SelectableUnit selectedUnit = null;
            foreach (RaycastHit hit in hits)
            {
                
                selectedUnit = hit.collider.gameObject.GetComponentInParent<SelectableUnit>();
                if (selectedUnit != null)
                {
                    
                    break;
                }
            }

            if (selectedUnit != null)
            {
                //seleccionamos algo
                if (!selectedUnits.Contains(selectedUnit))
                {
                    selectedUnits.Add(selectedUnit);
                }
            }
            else
            {
                //no seleccionamos
                if(!Input.GetKey(KeyCode.LeftControl))
                    Deselect();
            }

            foreach (SelectableUnit unit in selectedUnits)
            {
                unit.SelectUnit();
            }

        }

        private void Deselect()
        {
            foreach (SelectableUnit unit in selectedUnits)
            {
                unit.DeselectUnit();
            }
            selectedUnits.Clear();
        }



        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                selectionBoxImage.gameObject.SetActive(true);
                selectionBoxRect = new Rect();
                selectionStartPos = eventData.position;
                Debug.Log("Called");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
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
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
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
}
