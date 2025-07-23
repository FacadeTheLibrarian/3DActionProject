using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

internal sealed class ExpPointView : MonoBehaviour {
    [SerializeField] private Image _expPointBar = default;
    [SerializeField] private ParticleSystem _particleHandler = default;

    public void OnExpGained(float amount) {
        DOTween.To(() => _expPointBar.fillAmount, (x) => _expPointBar.fillAmount = x, amount, 0.5f);
    }

    public void OnGrowthReady() {
        _particleHandler.Play();
    }
}