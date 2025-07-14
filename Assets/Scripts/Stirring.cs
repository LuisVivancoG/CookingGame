using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stirring : MonoBehaviour
{
    [Header("Mixing Settings")]
    [SerializeField] private List<Transform> ingredientObjects;
    [SerializeField] private float mixRadius = 0.5f;
    [SerializeField] private float stirSensitivity = 0.2f;
    [SerializeField] private float positionJitterSpeed = 1.5f;

    private Vector2 _lastMouseDirection;
    private bool _isStirring;

    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (mouseDelta.magnitude > stirSensitivity)
        {
            Vector2 mouseDir = mouseDelta.normalized;

            // Check if movement is significantly changing direction (suggesting circular motion)
            float angle = Vector2.SignedAngle(_lastMouseDirection, mouseDir);

            if (Mathf.Abs(angle) > 30f && Mathf.Abs(angle) < 160f) // avoid sudden reversals
            {
                _isStirring = true;
                MixIngredients();
            }
            else
            {
                _isStirring = false;
            }

            _lastMouseDirection = mouseDir;
        }
    }

    void MixIngredients()
    {
        foreach (Transform ingredient in ingredientObjects)
        {
            Vector2 offset = Random.insideUnitCircle * mixRadius;

            Vector3 targetPos = ingredient.parent != null
                ? ingredient.parent.position + new Vector3(offset.x, offset.y, 0f)
                : ingredient.position + new Vector3(offset.x, offset.y, 0f);

            ingredient.position = Vector3.Lerp(ingredient.position, targetPos, Time.deltaTime * positionJitterSpeed);
        }
    }
}
