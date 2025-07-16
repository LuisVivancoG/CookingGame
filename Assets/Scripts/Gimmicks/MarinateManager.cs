using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarinateManager : MonoBehaviour
{
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
