using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    [Header("Drag Settings")]
    [SerializeField] private float screenXThreshold = 0f; // in screen space (pixels)
    [SerializeField] private float yMovementThreshold = 30f; // vertical movement in pixels to trigger event
    [SerializeField] private UnityEvent onVerticalMotion;

    private Camera _mainCamera;
    private Vector3 _originalPosition;
    private bool _isDragging = false;
    private bool _hasCrossedX = false;

    private float _lastY = -1f;
    private bool _wasMovingUp = false;

    private void Start()
    {
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        _hasCrossedX = false;
        _lastY = -1f;
        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (_isDragging)
        {
            // Convert mouse position to world space
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(_mainCamera.transform.position.z);
            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            transform.position = mouseWorldPos;

            // Check screen X threshold
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(transform.position);

            if (screenPos.x <= screenXThreshold)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 75f);
                _hasCrossedX = true;

                TrackVerticalMovement(screenPos.y);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    private void TrackVerticalMovement(float currentY)
    {
        if (_lastY < 0f)
        {
            _lastY = currentY;
            return;
        }

        float deltaY = currentY - _lastY;

        if (Mathf.Abs(deltaY) >= yMovementThreshold)
        {
            bool movingUp = deltaY > 0;

            // Only trigger when changing direction
            if (movingUp != _wasMovingUp)
            {
                onVerticalMotion?.Invoke();
                _wasMovingUp = movingUp;
                _lastY = currentY;
            }
        }
    }
}
