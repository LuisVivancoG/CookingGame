using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private float _delaySpawn;
    [SerializeField] private Transform _spawnerLoc;
    private Camera _mainCamera;
    private SquashAndStretch _squashComponent;
    private Vector2 _difference = Vector2.zero;
    private bool _isMoving;
    private Tween _rotationTween;
    private bool _isSpawning = false;

    private void Start()
    {
        _mainCamera = Camera.main;
        _squashComponent = GetComponent<SquashAndStretch>();
    }

    private void OnMouseDown()
    {
        _difference = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        _squashComponent.CheckForAndStartCoroutine();
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition) - _difference;
    }

    private void OnMouseUp()
    {
        _isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TriggerZone>(out TriggerZone component))
        {
            //Debug.Log($"Entering {collision.name}");

            _rotationTween?.Kill();

            var newRot = new Vector3(0, 0, 160);
            _rotationTween = transform.DORotate(newRot, .75f, RotateMode.Fast).SetEase(Ease.OutSine).OnComplete(() => _isMoving = false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isSpawning)
        {
            StartCoroutine(PoolObject());
        }
    }

    private IEnumerator PoolObject()
    {
        _isSpawning = true;

        yield return new WaitForSeconds(_delaySpawn);

        ObjectPooll.Instance.GetObject(_spawnerLoc.transform.position);

        _isSpawning = false;

        StartCoroutine(PoolObject());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<TriggerZone>(out TriggerZone component) )
        {
            //Debug.Log($"Exiting {collision.name}");
            StopAllCoroutines();
            _isSpawning = false;

            _rotationTween?.Kill();

            var newRot = new Vector3(0, 0, 0);
            _rotationTween = transform.DORotate(newRot, .9f, RotateMode.Fast).SetEase(Ease.OutSine).OnComplete(() => _isMoving = false);
        }
    }
}
