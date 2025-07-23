using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Create Scriptable Objects/New Recipe")]

public class RecipeSO : ScriptableObject
{
    [SerializeField] private string _dishName;
    [SerializeField] private ChoppableIngredients[] _choppableNeeded; //Ingredients to cut
    [SerializeField] private MarinatedCondiments[] _condimentsNeeded; //Condiments
    [SerializeField] private Sprite _spriteFinalDish;

    public string DishName => _dishName;
    public ChoppableIngredients[] ChoppableNeeded => _choppableNeeded;
    public MarinatedCondiments[] CondimentsNeeded => _condimentsNeeded;
    public Sprite SpriteDish => _spriteFinalDish;

    public Dictionary<ChoppableIngredients, int> _statusIngredients;


}
