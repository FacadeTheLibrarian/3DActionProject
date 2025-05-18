using UnityEngine;

internal sealed class Player : MonoBehaviour {
    [SerializeField] private Camera _mainCamera = default;

    [SerializeField] private MonsterSetData _monsterSetData = default;
    [SerializeField] private MonsterHandler _monsterHandler = default;

    [SerializeField] private AnimationStateController _animationStateController = default;
    [SerializeField] private PlayerDirection _direction = default;
    [SerializeField] private PlayerInputs _inputs = default;
    [SerializeField] private PlayerStamina _stamina = default;

    [SerializeField] private PlayerMove _move = default;
    [SerializeField] private PlayerAttack _attack = default;

#if UNITY_EDITOR
    private void Reset() {
        _direction = GetComponent<PlayerDirection>();
        _animationStateController = GetComponent<AnimationStateController>();
        _inputs = GetComponent<PlayerInputs>();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<PlayerAttack>();
        _stamina = GetComponent<PlayerStamina>();
    }
#endif

    private void Start() {
        _monsterHandler = new MonsterHandler(_monsterSetData, transform);
        _animationStateController.Initialization(_monsterHandler);
        _inputs.Initialization();
        _direction.Initialization(_mainCamera);
        _stamina.Initialization(_animationStateController);

        _move.Initialization(_inputs, _animationStateController, _direction);
        _attack.Initialization(_inputs, _animationStateController, _direction, _monsterHandler, _stamina);
    }

    private void Update() {
        _move.UpdateMove();
        _stamina.UpdateStamina();
    }

    private void OnDestroy() {
        _monsterHandler.Dispose();
        _animationStateController.Dispose();
    }
}