using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private GameObject _saltGO;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _minAngleParticles = 90f;
    [SerializeField] private float _maxAngleParticles = 270f;
    private bool _particlesActive = false;
    private void Update()
    {
        float zAngle = _saltGO.transform.eulerAngles.z;

        if (zAngle >= _minAngleParticles && zAngle < _maxAngleParticles)
        {
            if (!_particlesActive)
            {
                _particles.Play();
                _particlesActive = true;
            }
        }
        else
        {
            if (_particlesActive)
            {
                _particles.Stop();
                _particlesActive = false;
            }
        }
    }
}
