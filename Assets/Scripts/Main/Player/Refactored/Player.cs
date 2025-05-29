using UnityEngine;

internal sealed class Player : MonoBehaviour {
    [SerializeField] private Camera _mainCamera = default;

    [SerializeField] private MonsterData _monsterData = default;
    [SerializeField] private MonsterHandler _monsterHandler = default;

    [SerializeField] private AnimationStateController _animationStateController = default;
    [SerializeField] private PlayerDirection _direction = default;
    [SerializeField] private PlayerInputs _inputs = default;

    [SerializeField] private PlayerStamina _stamina = default;
    [SerializeField] private PlayerHitPoint _hitPoint = default;
    [SerializeField] private PlayerExpPoint _expPoint = default;
   
    [SerializeField] private PlayerMoveStateMachine _moveStateMachine = default;
    [SerializeField] private PlayerAttack _attack = default;

    [SerializeField] private PlayerGrowth _growth = default;
#if UNITY_EDITOR
    [SerializeField] private PlayerGeneration.e_generation _initialGenerationForDebug = PlayerGeneration.e_generation.first;
#endif
    private PlayerGeneration _generation = default;

#if UNITY_EDITOR
    private void Reset() {
        _direction = GetComponent<PlayerDirection>();
        _animationStateController = GetComponent<AnimationStateController>();
        _inputs = GetComponent<PlayerInputs>();
        _moveStateMachine = GetComponent<PlayerMoveStateMachine>();
        _attack = GetComponent<PlayerAttack>();
        _stamina = GetComponent<PlayerStamina>();
        _growth = GetComponent<PlayerGrowth>();
    }
#endif

    private void Awake() {
        _generation = new PlayerGeneration();
#if UNITY_EDITOR
        _generation.DebugSetGeneration = _initialGenerationForDebug;
#endif
        _monsterHandler = new MonsterHandler(_monsterData, transform, _generation);
        _expPoint.Initialization(_monsterData, _generation);

        _animationStateController.Initialization(_monsterHandler, _generation);
        _inputs.Initialization();
        _direction.Initialization(_mainCamera);
        _stamina.Initialization(_animationStateController, _monsterData);

        _moveStateMachine.Initialization(_inputs, _animationStateController, _direction, _monsterData, _stamina);
        _attack.Initialization(_inputs, _animationStateController, _direction, _monsterHandler, _stamina);

        _growth.Initialization(_inputs, _animationStateController, _monsterHandler, _expPoint);
    }

    private void Update() {
        _moveStateMachine.UpdateBehaviour();
        _stamina.UpdateStamina();
    }
}