using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        //_inputManager = InputManager.instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        _inputManager.OnEndTouch += Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _camera.nearClipPlane);
        Vector3 worldCoordinates = _camera.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;

        /*Collider2D hitCollider = Physics2D.OverlapPoint(worldCoordinates); //tray raycast
        if (hitCollider != null && hitCollider.TryGetComponent(out SliceSprite sprite)) //if this object is a SliceSprite proceeed
        {
            sprite.MakeSlice();
        }*/    
    }
}
