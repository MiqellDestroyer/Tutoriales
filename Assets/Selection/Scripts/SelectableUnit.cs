using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Selection
{
    public class SelectableUnit : MonoBehaviour
    {
        private bool selected = false;

        public bool IsSelected()
        {
            return selected;
        }

        public void SelectUnit()
        {
            selected = true;
        }

        public void DeselectUnit()
        {
            selected = false;
        }

        private void OnEnable()
        {
            SelectionController.OnBoxSelection += BoxSelection;
        }

        void BoxSelection()
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (SelectionController.selectionBoxRect.Contains(screenPosition))
            {
                if (!SelectionController.selectedUnits.Contains(this))
                {
                    SelectionController.selectedUnits.Add(this);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl) && IsSelected())
                {
                    if (!SelectionController.selectedUnits.Contains(this))
                    {
                        SelectionController.selectedUnits.Add(this);
                    }
                }
            }
        }
    }
}
