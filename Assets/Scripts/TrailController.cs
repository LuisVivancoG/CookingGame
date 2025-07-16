using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    private Camera mainCamera;
    private bool isDrawing = false;

    private void Start()
    {
        mainCamera = Camera.main;
        trail.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            trail.SetActive(true);
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(mainCamera.transform.position.z);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            trail.transform.position = worldPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            trail.SetActive(false);
        }
    }
}
