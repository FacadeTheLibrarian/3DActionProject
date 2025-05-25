using System.Collections.Generic;
using UnityEngine;

internal sealed class ExpPointPresenter : MonoBehaviour {
    [SerializeField] PlayerExpPoint _model = default;
    [SerializeField] ExpPointView _view = default;

    private void Awake() {
        _model.OnGainExp += _view.OnExpGained;
        _model.OnGrowthReady += _view.OnGrowthReady;
    }

    private void OnDestroy() {
        _model.OnGainExp -= _view.OnExpGained;
        _model.OnGrowthReady -= _view.OnGrowthReady;
    }
}