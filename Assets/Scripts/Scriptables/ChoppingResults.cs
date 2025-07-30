using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Create Scriptable Objects/Stage1Result SO")]

[System.Serializable]
public class ChoppedObjectData
{
    public ChoppingStates PrefabType;
}

public class ChoppingResults : ScriptableObject
{
    //public List<GameObject> ObjectsChopped = new List<GameObject>();

    public List<ChoppedObjectData> AssetsStored = new List<ChoppedObjectData>();

    //[SerializeField] private GameObject[] _prefabs;

    public void Clear()
    {
        AssetsStored.Clear();
    }
}
