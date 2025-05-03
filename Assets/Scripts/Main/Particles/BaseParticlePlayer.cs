using System.Collections.Generic;
using UnityEngine;

internal class BaseParticlePlayer : MonoBehaviour {
    [SerializeField] protected AnimationParticleSelecter _selecter = default;
    [SerializeField] private List<ParticleSystem> _particles = default;

    private void Start() {
        foreach (ParticleSystem particle in _particles) {
            particle.gameObject.SetActive(false);
        }
    }

    protected void InnerPlayParticle(int index) {
        _particles[index].gameObject.SetActive(true);
        _particles[index].Play();
    }
}