using System.Collections.Generic;
using UnityEngine;

internal sealed class ParticlePlayer : MonoBehaviour {
    [SerializeField] private AnimationParticleSelecter _selecter = default;
    [SerializeField] private List<ParticleSystem> _particles = default;

    private void Start() {
        foreach (ParticleSystem particle in _particles) {
            particle.gameObject.SetActive(false);
        }
    }

    public void Play(AnimationParticleSelecter.e_nagaParticles index) {
        _particles[(int)index].gameObject.SetActive(true);
        _particles[(int)index].Play();
    }
}