using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

internal sealed class HitPointView : MonoBehaviour {
    [SerializeField] private Image _hitPointBar = default;

    public void OnHitPointChanged(float amount) {
        DOTween.To(() => _hitPointBar.fillAmount, (x) => _hitPointBar.fillAmount = x, amount, 0.5f);
    }
}