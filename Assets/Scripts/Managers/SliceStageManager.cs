using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceStageManager : StageHandler
{
    [SerializeField] private RecipeSO _currentRecipe;
    private int _currentIngredient = 0;
    private Dictionary<int, GameObject> _choppingDictionary = new Dictionary<int, GameObject>();

    /*private void Start()
    {
        Initiate();
    }*/

    /*public override void Initiate()
    {
        for(int i = 0;  i < _currentRecipe.IngredientsToChop.Count; i++)
        {
            _choppingDictionary.Add(i, _currentRecipe.IngredientsToChop[i]);
            var choppablePrefab = Instantiate(_choppingDictionary[i]);
            choppablePrefab.SetActive(false);
        }
        _choppingDictionary[_currentIngredient].SetActive(true);
        Debug.Log("Enabling " + _choppingDictionary[_currentIngredient]);
    }

    public override void FetchNextIngredient()
    {
        if(_currentIngredient <= _choppingDictionary.Count)
        {
            _currentIngredient++;
            _choppingDictionary[_currentIngredient].SetActive(true);
        }
        else
        {
            GameManager.Instance.EndLoop();
        }
    }*/
}
