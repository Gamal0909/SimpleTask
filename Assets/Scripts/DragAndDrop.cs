using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class DragAndDrop : MonoBehaviour
    {
        private Camera _camera;
        private Backpack _backpack;
        private PickupItem _draggedItem;
        private Vector3 originalposition;

        private void Awake()
        {
            _camera=Camera.main;
            _backpack = FindObjectOfType<Backpack>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _draggedItem = hit.collider.GetComponent<PickupItem>();
                    if (_draggedItem != null)
                    {
                        originalposition = _draggedItem.transform.position;
                    }
                }
            }

            if (_draggedItem != null &&Input.GetMouseButton(0) )
            {
                Vector3 maousePosition = Input.mousePosition;
                maousePosition.z = 2f;
                _draggedItem.transform.position = _camera.ScreenToWorldPoint(maousePosition);
            }

            if (Input.GetMouseButtonUp(0) && _draggedItem != null)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Backpack backpack = hit.collider.GetComponent<Backpack>();
                    if (backpack != null)
                    {
                        backpack.AddItem(_draggedItem._itemData);
                        Destroy(_draggedItem.gameObject);
                    }
                    else
                    {
                        _draggedItem.transform.position = originalposition;
                    }

                    _draggedItem = null;
                }
            }
        }
    }
}