using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float _minimumDistance = 0.2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField, Range(0, 1f)] private float _directionThreshold = .9f;
    [SerializeField] private GameObject _trail;

    [SerializeField] private InputManager _inputManager;

    private Vector2 _startPos;
    private float _startTime;
    private Vector2 _endPos;
    private float _endTime;

    private Coroutine _coroutine;

    private void Start()
    {
        //_inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPos = position;
        _startTime = time;

        _trail.SetActive(true);
        _trail.transform.position = position;
        _coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            _trail.transform.position = _inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _trail.SetActive(false);
        StopCoroutine(_coroutine);

        _endPos = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPos, _endPos) >= _minimumDistance && (_endTime - _startTime) <= _maximumTime)
        {
            Debug.Log("Swipe detected");
            Debug.DrawLine(_startPos, _endPos, Color.red, 5f);
            Vector3 direction = _endPos - _startPos;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }
    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if(Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
    }
}
