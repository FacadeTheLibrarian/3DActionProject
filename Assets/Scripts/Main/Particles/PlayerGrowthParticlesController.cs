using System;
using System.Collections;
using UnityEngine;

internal sealed class PlayerGrowthParticlesController : MonoBehaviour {

    [SerializeField] private ParticleSystem _particleSystem = default;
    public void PlayParticle(Action callback, float timeAheadOfEnd) {
        float duration = _particleSystem.main.duration;
        _particleSystem.Play();
        StartCoroutine(OnParticlePlaying(duration, timeAheadOfEnd, callback));
    }

    private IEnumerator OnParticlePlaying(float duration, float timeAheadOfEnd, Action callback) {
        float timeToCall = duration - timeAheadOfEnd;
        float timeAccumeration = 0.0f;
        while (true) {
            timeAccumeration += Time.deltaTime;
            if (timeToCall < timeAccumeration) {
                Debug.Log("Action");
                callback.Invoke();
                yield break;
            }
            yield return null;
        }
    }
}