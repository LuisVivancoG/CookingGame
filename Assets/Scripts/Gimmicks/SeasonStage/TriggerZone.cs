using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _yRange = 2f;
    [SerializeField] private float _timeLength = 3f;
    [SerializeField] private Ease _easeMode;
    [SerializeField] private float _minAllowedY = -3f;

    private float _currentLenght;
    private float _minLenght;
    private float _cameraTopLimit;
    private float _objectHalfHeight;
    private Coroutine _movementCoroutine;
    private Vector3 _initialScale;

    private void Start()
    {
        _initialScale = transform.localScale;
        _currentLenght = _timeLength;
        _minLenght = _timeLength / 4;
        CalculateCameraLimit();
        UpdateHalfHeight();
        _movementCoroutine = StartCoroutine(MovementBegin());
    }

    private void CalculateCameraLimit()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        _cameraTopLimit = cam.transform.position.y + (camHeight / 2f);
    }

    private void UpdateHalfHeight()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            _objectHalfHeight = col.bounds.extents.y;
        }
        else
        {
            _objectHalfHeight = (transform.localScale.y / 2f);
        }
        
        if(_currentLenght > _minLenght)
        {
            _currentLenght -= _minLenght;
        }
    }

    IEnumerator MovementBegin()
    {
        while (true)
        {
            float newY = GetNewPos();
            transform.DOMoveY(newY, _currentLenght / 2f).SetEase(_easeMode);
            yield return new WaitForSeconds(_currentLenght);
        }
    }

    private float GetNewPos()
    {
        float currentY = transform.position.y;

        float minY = Mathf.Max(_minAllowedY + _objectHalfHeight, currentY - _yRange);
        float maxY = Mathf.Min(_cameraTopLimit - _objectHalfHeight, currentY + _yRange);

        return Random.Range(minY, maxY);
    }

    public void AdjustHeight()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = transform.localScale.y - (_initialScale.y / 4);
        transform.localScale = newScale;

        UpdateHalfHeight();
    }
}
