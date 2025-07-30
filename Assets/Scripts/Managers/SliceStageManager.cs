using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class SliceStageManager : StageHandler
{
    [SerializeField] private RecipeSO _currentRecipe;
    [SerializeField] private Transform _instancesParent;

    [SerializeField] private ChoppingResults _results;

    private int _currentIndex = 0;
    private Dictionary<int, GameObject> _choppingDictionary = new Dictionary<int, GameObject>();

    private void Start()
    {
        Initiate();
    }

    public override void Initiate()
    {
        for(int i = 0;  i < _currentRecipe.IngredientsToChop.Count; i++)
        {
            var choppableInstance = Instantiate(_currentRecipe.IngredientsToChop[i], _instancesParent);
            choppableInstance.SetActive(false);
            choppableInstance.TryGetComponent<SliceIngredient>(out SliceIngredient slice);
            slice.SetStageManager(this, _instancesParent);
            _choppingDictionary.Add(i, choppableInstance);
        }
    }

    public override void FetchNextIngredient()
    {
        if (_currentIndex < _choppingDictionary.Count)
        {
            _choppingDictionary[_currentIndex].gameObject.SetActive(true);
            _currentIndex++;
        }
        else
        {
            Debug.Log("Current index out of dictionary" + _currentIndex);
            GameManager.Instance.EndLoop();
        }
    }

    public void SaveParent()
    {
        _results.Clear();

        var count = _instancesParent.childCount;
        Debug.Log($"There are {count} childs in the parent");

        foreach (Transform childTransform in _instancesParent.transform)
        {
            if (TryGetChoppingStateFromName(childTransform.name, out ChoppingStates state))
            {
                _results.AssetsStored.Add(new ChoppedObjectData { PrefabType = state });
            }
            else
            {
                Debug.LogWarning($"Could not determine chopping state for {childTransform.name}");
            }
        }

        Debug.Log($"Saved chopped objects: {_results.AssetsStored.Count}");

        GameManager.Instance.LoadNextLevel();
    }
    private bool TryGetChoppingStateFromName(string name, out ChoppingStates state)
    {
        name = name.Replace("(Clone)", "").Trim();
        return Enum.TryParse(name, out state);
    }
}
