using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

internal sealed class Player : MonoBehaviour {
    [SerializeField] private Camera _mainCamera = default;

    [SerializeField] private MonsterSetData _monsterSetData = default;
    [SerializeField] private MonsterHandler _monsterHandler = default;

    [SerializeField] private PlayerDirection _direction = default;
    [SerializeField] private AnimationStateController _animator = default;
    [SerializeField] private PlayerInputs _inputs = default;

    [SerializeField] private PlayerMove _move = default;
    [SerializeField] private PlayerAttack _attack = default;

#if UNITY_EDITOR
    private void Reset() {
        _direction = GetComponent<PlayerDirection>();
        _animator = GetComponent<AnimationStateController>();
        _inputs = GetComponent<PlayerInputs>();
        _move = GetComponent<PlayerMove>();
    }
#endif

    private void Start() {
        _monsterHandler = new MonsterHandler(_monsterSetData, transform);
        _animator.Initialization(_monsterHandler);
        _inputs.SetUp();
        _direction.SetUp(_mainCamera);

        _move.Initialization(_inputs, _animator, _direction);
        _attack.Initialization(_inputs, _animator, _direction, _monsterHandler);
    }

    private void Update() {
        _move.UpdateMove();
    }

    private void OnDestroy() {
        _monsterHandler.Dispose();
        _animator.Dispose();
    }
}