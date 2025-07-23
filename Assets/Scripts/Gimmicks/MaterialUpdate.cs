using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MaterialUpdate : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _cutSize = 0.5f;
    private float currentValue;

    private void OnEnable()
    {
        RestoreValue();

        currentValue = _material.GetFloat("_Edge_1");

        //Debug.Log("Current value " + _material.GetFloat("_Edge_1"));
    }

    private void OnDisable()
    {
        RestoreValue();
    }

    public void makeSlice()
    {
        var newValue = currentValue -= _cutSize;

        _material.SetFloat("_Edge_1", newValue);

        //Debug.Log("Slice made " + _material.GetFloat("_Edge_1"));
    }

    public void RestoreValue()
    {
        _material.SetFloat("_Edge_1", 1.5f);
    }
}
