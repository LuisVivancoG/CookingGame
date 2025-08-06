using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    [Header("Drag Settings")]
    [SerializeField] private float screenXThreshold = 0f;
    [SerializeField] private float yMovementThreshold = 30f;
    [SerializeField] private UnityEvent onVerticalMotion;

    private Camera _mainCamera;
    private Vector3 _originalPosition;
    private bool _isDragging = false;

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
        _lastY = -1f;
        transform.position = _originalPosition;
    }

    private void Update()
    {
        if (_isDragging)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(_mainCamera.transform.position.z);
            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            transform.position = mouseWorldPos;

            Vector3 screenPos = _mainCamera.WorldToScreenPoint(transform.position);

            if (screenPos.x <= screenXThreshold)
            {
                TrackVerticalMovement(screenPos.y);
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

            if (movingUp != _wasMovingUp)
            {
                onVerticalMotion?.Invoke();
                _wasMovingUp = movingUp;
                _lastY = currentY;
            }
        }
    }
}
