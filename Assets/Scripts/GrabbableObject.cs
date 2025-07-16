using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class GrabbableObject : MonoBehaviour
{
    [Header("Seasoning Zone Settings")]
    [SerializeField] private Vector2 seasoningZoneCenter = new Vector2(0.5f, 0.5f);
    [SerializeField] private float seasoningZoneWidth = 0.2f;
    [SerializeField] private float seasoningZoneHeight = 0.4f;

    [Header("Shake Sensitivity")]
    [SerializeField] private float minShakeDeltaY = 0.05f;
    [SerializeField] private float maxShakeDeltaY = 0.3f;
    [SerializeField] private float multiplierValue = 7;

    [SerializeField] private FloatEvent OnSeasoningShakeWithStrength;

    private Camera _mainCamera;
    private Vector3 _originalPosition;
    private bool _isDragging = false;
    private float _lastY = -1f;
    private bool _waitingForDown = false;
    private SquashAndStretch SquashComponent;

    private void Start()
    {
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
        SquashComponent = GetComponent<SquashAndStretch>();
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        SquashComponent.CheckForAndStartCoroutine();
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        _lastY = -1f;
        _waitingForDown = false;
        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (!_isDragging) return;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(_mainCamera.transform.position.z);
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;
        transform.position = mouseWorldPos;

        Vector3 viewportPos = _mainCamera.WorldToViewportPoint(transform.position);
        bool insideZone = IsInsideSeasoningZone(viewportPos);

        if (insideZone)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 135f);
            SquashComponent.CheckForAndStartCoroutine();
            DetectShake(viewportPos.y);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private bool IsInsideSeasoningZone(Vector3 viewportPos)
    {
        float dx = Mathf.Abs(viewportPos.x - seasoningZoneCenter.x);
        float dy = Mathf.Abs(viewportPos.y - seasoningZoneCenter.y);
        return dx <= seasoningZoneWidth * 0.5f && dy <= seasoningZoneHeight * 0.5f;
    }

    private void DetectShake(float currentY)
    {
        if (_lastY < 0f)
        {
            _lastY = currentY;
            return;
        }

        float deltaY = currentY - _lastY;

        if (!_waitingForDown && deltaY > minShakeDeltaY)
        {
            _waitingForDown = true;
        }
        else if (_waitingForDown && deltaY < -minShakeDeltaY)
        {
            float shakeStrength = Mathf.Abs(deltaY);

            // Clamp and normalize between min and max
            float normalized = Mathf.InverseLerp(minShakeDeltaY, maxShakeDeltaY, shakeStrength);
            normalized = normalized * multiplierValue;


            OnSeasoningShakeWithStrength?.Invoke(normalized);

            _waitingForDown = false;
        }

        _lastY = currentY;
    }
}
