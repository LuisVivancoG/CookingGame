using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SliceSprite : MonoBehaviour
{
    [SerializeField] private MaterialUpdate _mat;
    [SerializeField] private List<GameObject> _instances;
    [SerializeField] private Vector3 _newPos;
    private bool _multipleInstances;

    private void Start()
    {
        if (_instances.Count > 1)
        {
            _multipleInstances = true;
        }
    }

    public void MakeSlice(Transform ParentGO)
    {
        _mat.makeSlice();

        if (!_multipleInstances)
        {
            GetSlicedPiece(0, ParentGO);
        }
        else
        {
            GetSlicedPiece(RandomIndex(), ParentGO);
        }
    }

    private void GetSlicedPiece(int index, Transform Parent)
    {
        var instance = Instantiate(_instances[index], Parent.transform);
    }

    private int RandomIndex()
    {
        return Random.Range(0, _instances.Count);
    }
}
