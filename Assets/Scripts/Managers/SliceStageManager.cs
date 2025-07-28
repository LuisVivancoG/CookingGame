using System.Collections.Generic;
using UnityEngine;

public class SliceStageManager : StageHandler
{
    [SerializeField] private RecipeSO _currentRecipe;
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
            var choppableInstance = Instantiate(_currentRecipe.IngredientsToChop[i]);
            choppableInstance.SetActive(false);
            choppableInstance.TryGetComponent<SliceIngredient>(out SliceIngredient slice);
            slice.SetStageManager(this);
            _choppingDictionary.Add(i, choppableInstance);
        }
        //Debug.Log($"Dictionary filled with {_choppingDictionary.Count} ingredients.");
    }

    public override void FetchNextIngredient()
    {
        if (!_choppingDictionary.ContainsKey(_currentIndex))
        {
            //Debug.LogWarning($"Key {_currentIndex} not found in dictionary!");
            return;
        }

        //Debug.Log("Fetching for " + _choppingDictionary[_currentIndex] + " / currentIndex = " + _currentIndex + " / dictionary count = " + _choppingDictionary.Count);

        if (_currentIndex <= _choppingDictionary.Count)
        {
            //Debug.Log("Activating " + _currentIndex + _choppingDictionary[_currentIndex]);
            _choppingDictionary[_currentIndex].gameObject.SetActive(true);
            _currentIndex++;
        }
        else
        {
            Debug.Log("Current index out of dictionary" + _currentIndex);
            GameManager.Instance.EndLoop();
        }
    }
}
