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
    private MonsterHandler _monsterHandler = default;
    private PlayerGeneration _generation = default;

    public void Initialization(in PlayerInputs input, in AnimationStateController animationStateController, in MonsterHandler handler, in PlayerExpPoint expPoint, in PlayerGeneration generation) {
        _growthAction = input[PlayerInputs.e_inputActions.growth];
        _growthAction.started += OnEnterGrowth;
        _growthAction.Enable();
        _animationStateController = animationStateController;
        _monsterHandler = handler;
        _playerExpPoint = expPoint;
        _generation = generation;
    }

    private void OnDestroy() {
        _growthAction.started -= OnEnterGrowth;
        _growthAction.Disable();
    }

    private void OnEnterGrowth(InputAction.CallbackContext context) {
        if (!_playerExpPoint.TryGrowth()) {
            return;
        }
        _growthTimeline.Play();
    }
#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            _playerExpPoint.DebugGrowth();
            _monsterHandler.Increment((int)_generation.GetCurrentGeneration);
            _generation.GrowthToNextGeneration();
        }
    }
#endif
}