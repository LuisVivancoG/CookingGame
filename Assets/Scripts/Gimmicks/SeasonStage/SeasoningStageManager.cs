using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasoningStageManager : StageHandler
{
    [SerializeField] private ChoppingResults _previousResults;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private GameObject _parentInstances;

    [Header("Score System")]
    [SerializeField] private float _goalScore = 5f;
    private float _score = 0f;

    [Header("UI and FX")]
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private ParticleSystem _saltParticles;

    [Header("Ideal Seasoning Range")]
    [SerializeField] private float _minRange = 0.55f;
    [SerializeField] private float _maxRange = 0.65f;

    [Header("Scrollbar Settings")]
    [SerializeField] private float _barSpeed = 1f;

    private float _targetValue = 0f;
    public float MinRange => _minRange;
    public float MaxRange => _maxRange;

    private void Start()
    {
        Initiate();
    }

    public override void Initiate()
    {
        foreach (var asset in _previousResults.AssetsStored)
        {
            int index = (int)asset.PrefabType;

            if (index >= 0 && index < _prefabs.Length && _prefabs[index] != null)
            {
                var instance = Instantiate(_prefabs[index], _parentInstances.transform);
                instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
                rb.gravityScale = 1;
            }
            else
            {
                Debug.LogWarning($"Invalid prefab index: {index} for {asset.PrefabType}");
            }
        }
    }

    public override void FetchNextIngredient()
    {

    }
    public void ReceiveSeasoningStrength(float strength)
    {
        _score += strength;
        _score = Mathf.Min(_score, _goalScore);

        _targetValue = Mathf.Clamp01(_score / _goalScore);

        bool inRange = strength >= _minRange && strength <= _maxRange;

        if (inRange)
        {
            // Emit based on shake strength
            int particlesToEmit = Mathf.RoundToInt(strength * 10f); // e.g. 0.5 -> 5 particles
            _saltParticles.Emit(particlesToEmit);
        }

        Debug.Log($"Strength: {strength:F2} | Score: {_score:F2}/{_goalScore}");
    }

    private void Update()
    {
        // Smoothly animate scrollbar value
        if (_scrollbar != null)
        {
            _scrollbar.value = Mathf.MoveTowards(_scrollbar.value, _targetValue, Time.deltaTime * _barSpeed);
        }

        // Stop FX if goal is reached
        if (_score >= _goalScore && _saltParticles.isPlaying)
        {
            _saltParticles.Stop();
            Debug.Log("Sazonado completo");
        }
    }
}
