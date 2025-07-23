using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

internal sealed class StaminaView : MonoBehaviour {
    [SerializeField] private Image _staminaBar = default;

    public void OnStaminaChanged(float amount) {
        DOTween.To(() => _staminaBar.fillAmount, (x) => _staminaBar.fillAmount = x, amount, 0.5f);
    }
}