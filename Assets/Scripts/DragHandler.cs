using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class DragHandler : MonoBehaviour
    {
        private Camera mainCamera;
        private bool isDragging = false;
        private Vector3 offset;

        private void Start()
        {
            mainCamera=Camera.main;
        }

        private void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
            return mainCamera.ScreenToWorldPoint(mousePos);
        }

        private void OnMouseUp() => isDragging = false;

        void Update()
        {
            if (isDragging)
            {
                transform.position = GetMouseWorldPosition() + offset;
            }
        }
    }
}