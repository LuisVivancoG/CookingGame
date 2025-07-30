using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputDemo : MonoBehaviour
{
    public UnityEvent _slicedCheese;
    public UnityEvent _seasoned;
    private Camera _mainCamera;

    private CinemachineImpulseSource _impulseSource;
    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
            Vector2 mouseWorld2D = new Vector2(worldPos.x, worldPos.y);

            Debug.DrawLine(mouseWorld2D, mouseWorld2D + Vector2.up * 0.5f, Color.red, 1f);

            Collider2D hit = Physics2D.OverlapPoint(mouseWorld2D);

            if (hit != null)
            {
                if (hit.TryGetComponent(out SliceSprite sliceable))
                {
                    //sliceable.MakeSlice();
                    _slicedCheese?.Invoke();
                    CameraShake.Instance.DoShake(_impulseSource);
                    Debug.Log("Sliced: " + hit.name);
                }
                else
                {
                    Debug.Log("Clicked object doesn't have SliceSprite: " + hit.name);
                }
            }
            else
            {
                Debug.Log("No object detected under mouse.");
            }
        }
    }
}
