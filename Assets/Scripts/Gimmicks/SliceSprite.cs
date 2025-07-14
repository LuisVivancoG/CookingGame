using UnityEngine;

public class SliceSprite : MonoBehaviour
{
    [SerializeField] private GameObject _mask;
    [SerializeField] private float _sliceSize;
    public bool InvertAxis = false;
    private Vector3 _currentPos;

    private void Start()
    {
        _currentPos = _mask.transform.position;
    }

    public void MakeSlice()
    {
        Debug.Log("Sliced");

        _currentPos = new Vector3(_currentPos.x - _sliceSize, _currentPos.y, _currentPos.z);

        _mask.gameObject.transform.position = _currentPos;
    }

    public void Season()
    {
        Debug.Log("Sliced");

        _currentPos = new Vector3(_currentPos.x, _currentPos.y - _sliceSize, _currentPos.z);

        _mask.gameObject.transform.position = _currentPos;
    }

}
