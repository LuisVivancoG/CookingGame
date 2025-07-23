using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Create Scriptable Objects/New Ingredient")]

public class FoodSlicedSO : ScriptableObject
{
    [SerializeField] private ChoppableIngredients _type;
    [SerializeField] private Sprite _ingredientSprite;
    [SerializeField] private GameObject _fullPrefab;
    [SerializeField] private GameObject _thirdPrefab;
    [SerializeField] private GameObject _secondPrefab;
    [SerializeField] private GameObject _lastPrefab;
    [SerializeField] private GameObject _cutPrefab;

    public ChoppableIngredients Type => _type;
    public Sprite IngredientSprite => _ingredientSprite;
    public GameObject FullPrefab => _fullPrefab;
    public GameObject ThirdPrefab => _thirdPrefab;
    public GameObject SecondPrefab => _secondPrefab;
    public GameObject LastPrefab => _lastPrefab;
    public GameObject CutPrefab => _cutPrefab;
}
