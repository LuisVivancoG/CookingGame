using System.Collections.Generic;
using UnityEngine;

public class ParticlesObserver : MonoBehaviour
{
    /*[SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Collider _triggerZone;

    [SerializeField] private int _threshold = 25;

    private int _totalParticles = 0;
    private int _nextThreshold = 25;

    private List<ParticleSystem.Particle> _particles = new List<ParticleSystem.Particle>();
    private int _countInZone = 0;

    void Start()
    {
        if (_particleSystem == null || _triggerZone == null)
        {
            Debug.LogError("ParticleSystem o TriggerZone no asignado.");
            return;
        }

        var triggerModule = _particleSystem.trigger;
        triggerModule.enabled = true;
        triggerModule.SetCollider(0, _triggerZone);
    }

    void LateUpdate()
    {
        _particles.Clear();
        int numEnter = _particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);

        _totalParticles += numEnter;

        // Cada vez que alcanzamos un múltiplo de 25, sumamos un punto
        while (_totalParticles >= _nextThreshold)
        {
            SeasoningStageManager.Instance.FetchNextIngredient();
            _nextThreshold += _threshold;
        }

        Debug.Log($"Total acumulado: {_totalParticles} | Siguiente umbral: {_nextThreshold}");
    }*/
}
