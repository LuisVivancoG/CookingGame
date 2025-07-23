using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Create Scriptable Objects/New Recipe")]

public class RecipeSO : ScriptableObject
{
    [SerializeField] private string _dishName;

    /*[System.Serializable]
    public class Ingredient
    {
        [SerializeField] private ChoppableIngredients Type;
        [SerializeField] private GameObject ChoppablePrefab;
    }*/

    [SerializeField] private List<GameObject> _ingredientsToChop;
    [SerializeField] private MarinatedCondiments[] _condimentsNeeded; //Condiments
    [SerializeField] private Sprite _spriteFinalDish;

    public string DishName => _dishName;
    public List<GameObject> IngredientsToChop => _ingredientsToChop;
    public MarinatedCondiments[] CondimentsNeeded => _condimentsNeeded;
    public Sprite SpriteDish => _spriteFinalDish;

    public Dictionary<ChoppableIngredients, int> _statusIngredients;


}
