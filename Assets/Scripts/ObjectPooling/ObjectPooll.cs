using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooll : Singleton<ObjectPooll>
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _parent;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private SeasoningStageManager _stageManager;

    private void Start()
    {
        _stageManager = FindAnyObjectByType<SeasoningStageManager>();
    }

    public GameObject GetObject()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            Debug.Log("Retrieving previous obj");
            obj.SetActive(true);
            return obj;
        }

        var newObj = Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation, _parent.transform);
        Debug.Log("New object pooled");
        newObj.TryGetComponent<Collectable>(out Collectable component);
        component.SetPool(this, _stageManager);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
