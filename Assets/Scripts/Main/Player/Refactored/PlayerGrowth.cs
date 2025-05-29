using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

internal sealed class PlayerGrowth : MonoBehaviour {

    [SerializeField] private PlayableDirector _growthTimeline = default;
    private PlayerExpPoint _playerExpPoint = default;

    private InputAction _growthAction = default;
    private AnimationStateController _animationStateController = default;

    public void Initialization(in PlayerInputs input, in AnimationStateController animationStateController, in MonsterHandler handler, in PlayerExpPoint expPoint) {
        _growthAction = input[PlayerInputs.e_inputActions.growth];
        _growthAction.started += OnEnterGrowth;
        _growthAction.Enable();
        _animationStateController = animationStateController;
        _playerExpPoint = expPoint;
    }

    private void OnDestroy() {
        _growthAction.started -= OnEnterGrowth;
        _growthAction.Disable();
    }

    private void OnEnterGrowth(InputAction.CallbackContext context) {
        //if (!_playerExpPoint.IsGrowthReady) {
        //    return;
        //}
        _growthTimeline.Play();
    }
}