using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//TODO: ステートパターンが適していると思うので、それに沿って変更
internal sealed class PlayerGodClass : MonoBehaviour {
    //NOTE: 接地判定定数
    private const float GROUND_OFFSET = -0.5f;
    private const float GROUNDED_RADIUS = 0.5f;

    //NOTE: 移動関連定数
    private const float UNITY_DEGREE_ADJUSTMENT = 90.0f;
    private const float MOVE_SPEED = 10.0f;
    private const float SPRINT_SPEED = 20.0f;
    private const float STOP_THRESHOLD = 0.125f;
    private const float SPEED_CHANGE_RATE = 0.0625f;

    //NOTE: 落下関連定数
    private const float FALL_SPEED_ADDEND = 10.0f;
    private const float FALL_TERMINAL_VELOCITY = 10.0f;

    //NOTE: 回避関連定数
    private const float DODGE_INITIAL_FORCE = 30.0f;
    private const float DODGE_TIME = 1 / 0.5f;

    //NOTE: スタミナ系定数
    private const float STAMINA_MAX = 100.0f;
    private const float STAMINA_BASE_RECOVERY_AMOUNT = 20.0f;
    private const float STAMINA_BASE_CONSUMPTION_ON_SPRINT = 0.5f;
    private const float STAMINA_BASE_CONSUMPTION_ON_DODGE = 40.0f;
    private const float STAMINA_BASE_CONSUMPTION_ON_ATTACK = 30.0f;

    //NOTE: キャスト時系定数
    private const int CAST_BUFFER_SIZE = 16;

    //NOTE: 依存コンポーネント
    [SerializeField] private Camera _mainCamera = default;
    [SerializeField] private Animator _animator = default;

    [SerializeField] private InputActionReference _moveAction = default;
    [SerializeField] private InputActionReference _sprintAction = default;
    [SerializeField] private InputActionReference _dodgeAction = default;
    [SerializeField] private CharacterController _controller = default;

    //NOTE: 共通
    [SerializeField] private Vector3 _forward = default;

    //NOTE: 成長システム関連
    [SerializeField] private int _currentExp = 0;

    //NOTE: 移動関連
    [SerializeField] private float _currentHorizontalVelocity = 0.0f;
    [SerializeField] private float _verticalVelocity = 0.0f;
    [SerializeField] private float _currentMoveSpeed = MOVE_SPEED;
    [SerializeField] private float _currentSprintSpeed = SPRINT_SPEED;
    [SerializeField] private LayerMask _layer = default;
    [SerializeField] private bool _isOnGround = false;

    [SerializeField] private bool _hasMoveInput = false;
    [SerializeField] private bool _hasSprintInput = false;

    [SerializeField] private bool _onStop = true;

    //NOTE: 回避関連
    [SerializeField] private bool _onDodge = false;
    [SerializeField] private float _dodgeForce = 0.0f;

    //NOTE: スタミナ関連
    [SerializeField] private float _stamina = STAMINA_MAX;
    [SerializeField] private bool _isRunOutOfStamina = false;

    //NOTE: ミュータブル化
    [SerializeField] private float _staminaConsumptionOnSprint = STAMINA_BASE_CONSUMPTION_ON_SPRINT;
    [SerializeField] private float _staminaConsumptionOnDodge = STAMINA_BASE_CONSUMPTION_ON_DODGE;
    [SerializeField] private float _staminaConsumptionOnAttack = STAMINA_BASE_CONSUMPTION_ON_ATTACK;
    [SerializeField] private float _staminaMax = STAMINA_MAX;
    [SerializeField] private float _staminaRecoveryAmount = STAMINA_BASE_RECOVERY_AMOUNT;

    [SerializeField] private Image _staminaBar = default;

    //NOTE: 攻撃関連
    [SerializeField] private InputActionReference _normalAttackAction = default;
    [SerializeField] private InputActionReference _specialAttackAction = default;

    [SerializeField] private BaseOnAttackAction[] _attackState = default;

    [SerializeField] private bool _isOnAttack = false;

#if UNITY_EDITOR
    private void Reset() {
        _mainCamera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }
#endif

    public void PlayerStart() {
        _moveAction.action.started += StartMove;
        _moveAction.action.canceled += EndMove;

        _sprintAction.action.started += StartSprint;
        _sprintAction.action.canceled += EndSprint;

        _dodgeAction.action.started += StartDodge;
        _moveAction.action.Enable();
        _sprintAction.action.Enable();
        _dodgeAction.action.Enable();

        _normalAttackAction.action.started += NormalAttack;
        _normalAttackAction.action.Enable();

        _specialAttackAction.action.started += SpecialAttack;
        _specialAttackAction.action.Enable();

        _attackState = _animator.GetBehaviours<BaseOnAttackAction>();
        foreach (BaseOnAttackAction behaviour in _attackState) {
            behaviour.Initialization(this);
            behaviour.OnStartAttackPublisher += AttackCast;
            behaviour.OnEndAttackPublisher += EndAttack;
        }
    }

    private void OnDestroy() {
        _moveAction.action.started -= StartMove;
        _moveAction.action.canceled -= EndMove;
        _moveAction.action.Dispose();

        _sprintAction.action.started -= StartSprint;
        _sprintAction.action.canceled -= EndSprint;
        _sprintAction.action.Dispose();

        _normalAttackAction.action.started -= NormalAttack;
        _normalAttackAction.action.Dispose();

        _specialAttackAction.action.started -= SpecialAttack;
        _specialAttackAction.action.Dispose();

        foreach (BaseOnAttackAction behaviour in _attackState) {
            behaviour.OnStartAttackPublisher -= AttackCast;
            behaviour.OnEndAttackPublisher -= EndAttack;
        }
    }

    //共通
    private void UpdateForward() {
        Vector2 direction = _moveAction.action.ReadValue<Vector2>();
        //TODO: カメラがイベント発行者のリアクティブプロパティにしたらどうだろうか検討
        float cameraAngle = _mainCamera.transform.localRotation.eulerAngles.y;

        float radian = Mathf.Atan2(direction.y, -direction.x) + cameraAngle * Mathf.Deg2Rad;
        float degree = radian * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, degree - UNITY_DEGREE_ADJUSTMENT, this.transform.rotation.eulerAngles.z);

        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        _forward.Set(-x, 0.0f, y);
    }

    //移動関連
    public void HorizontalMove() {
        UpdateVerticalSpeed();
        if (_onStop) {
            return;
        }
        float targetSpeed = 0.0f;
        //FIXME: なおして
        if (!_isOnAttack) {
            if (_hasMoveInput) {
                if (_hasSprintInput) {
                    UseStamina(STAMINA_BASE_CONSUMPTION_ON_SPRINT);
                }
                targetSpeed = _hasSprintInput ? _currentSprintSpeed : _currentMoveSpeed;
                UpdateForward();
            }
        }

        _currentHorizontalVelocity = Mathf.Lerp(_currentHorizontalVelocity, targetSpeed, SPEED_CHANGE_RATE);
        Vector3 force = new Vector3(_forward.x * _currentHorizontalVelocity, -_verticalVelocity, _forward.z * _currentHorizontalVelocity);
        _animator.SetFloat("Speed", _currentHorizontalVelocity / _currentSprintSpeed);
        _controller.Move(force * Time.deltaTime);

        if (_hasMoveInput) {
            return;
        }
        if (MathUtility.IsInsideExclusive(_currentHorizontalVelocity, -STOP_THRESHOLD, STOP_THRESHOLD)) {
            _animator.SetFloat("Speed", 0.0f);
            _currentHorizontalVelocity = 0.0f;
            _onStop = true;
        }
    }

    public void UpdateVerticalSpeed() {
        _isOnGround = GroundedCheck(this.transform.position);
        if (_isOnGround) {
            _verticalVelocity = 0.0f;
            return;
        }
        if (_verticalVelocity < FALL_TERMINAL_VELOCITY) {
            _verticalVelocity += FALL_SPEED_ADDEND * Time.deltaTime;
        }
    }

    //NOTE: 設置判定関連
    public bool GroundedCheck(in Vector3 currentPosition) {
        Vector3 spherePosition = new Vector3(currentPosition.x, currentPosition.y - GROUND_OFFSET,
            currentPosition.z);
        return Physics.CheckSphere(spherePosition, GROUNDED_RADIUS, _layer,
            QueryTriggerInteraction.Ignore);
    }


    //NOTE: 回避関連
    private void StartDodge(InputAction.CallbackContext context) {
        if (_isRunOutOfStamina) {
            return;
        }
        if (_onDodge) {
            return;
        }
        UpdateForward();
        _controller.excludeLayers = _layerOnDodge;
        _dodgeForce = DODGE_INITIAL_FORCE;
        UseStamina(STAMINA_BASE_CONSUMPTION_ON_DODGE);
        _animator.Play("Dodge");
        _isOnAttack = false;
        _onDodge = true;
        _onStop = true;
    }

    //FIXME: Dodgeに関してはUniTaskも検討
    public void Dodge() {
        if (!_onDodge) {
            return;
        }
        Vector3 force = new Vector3(_forward.x * _dodgeForce, -_verticalVelocity, _forward.z * _dodgeForce);
        _controller.Move(force * Time.deltaTime);
        _dodgeForce -= DODGE_INITIAL_FORCE * DODGE_TIME * Time.deltaTime;

        if (_dodgeForce <= 0.0f) {
            _animator.SetTrigger("EndDodge");
            _controller.excludeLayers = 0;
            _currentHorizontalVelocity = _dodgeForce;
            _onDodge = false;
            _onStop = false;
        }
    }

    //NOTE: スタミナ関連
    private void UseStamina(float amount) {
        _stamina -= amount;
        if (_stamina <= 0.0f) {
            //NOTE: ここイベントでいいかも？
            _hasSprintInput = false;
            _isRunOutOfStamina = true;
            _stamina = 0.0f;
        }
    }

    //FIXME: スタミナ消費後のウェイトタイムを設ける
    public void RecoverStamina() {
        if (_onDodge) {
            return;
        }
        if (_hasSprintInput) {
            return;
        }
        if (_stamina >= _staminaMax) {
            return;
        }
        _stamina += _staminaRecoveryAmount * Time.deltaTime;
        if (_stamina >= _staminaMax) {
            _isRunOutOfStamina = false;
            _stamina = _staminaMax;
        }
    }

    public void UpdateStaminaBar() {
        _staminaBar.fillAmount = _stamina / _staminaMax;
    }

    //NOTE: 攻撃関連

    //FIXME: Attackの種類のストラテジとキーを持ったディクショナリを作成する
    //UPDATE: 攻撃をStateMachineBehaviourに持たせる
    private void AttackCast() {
        UseStamina(_staminaConsumptionOnAttack);
        Vector3 forwardedPosition = (_forward * 3.0f);
        Vector3 height = new Vector3(0.0f, this.transform.localScale.y / 2.0f, 0.0f);
        Vector3 castPosition = this.transform.position + height + forwardedPosition;
        Collider[] results = new Collider[CAST_BUFFER_SIZE];
        int numberOfCollider = Physics.OverlapSphereNonAlloc(castPosition, 2.0f, results, _layer);
        //Physics.SphereCastNonAlloc(this.transform.position, 2.0f, _forward, results, 10.0f, _layer);

        if (numberOfCollider == 0) {
            return;
        }

        for (int i = 0; i < numberOfCollider; i++) {
            if (results[i].TryGetComponent<IEnemy>(out IEnemy possibleEnemy)) {
                possibleEnemy.GetHit(10, _forward);
            }
        }
    }

    //NOTE: Input Systemイベント用
    private void NormalAttack(InputAction.CallbackContext context) {
        if (_isRunOutOfStamina) {
            return;
        }
        _isOnAttack = true;
        _animator.SetTrigger("Attack");
        _animator.SetInteger("AttackType", 0);
    }

    private void SpecialAttack(InputAction.CallbackContext context) {
        if (_isRunOutOfStamina) {
            return;
        }
        _isOnAttack = true;
        _animator.SetTrigger("Attack");
        _animator.SetInteger("AttackType", 1);
    }

    private void StartMove(InputAction.CallbackContext context) {
        _onStop = false;
        _hasMoveInput = true;
    }
    private void EndMove(InputAction.CallbackContext context) {
        _hasMoveInput = false;
    }
    private void StartSprint(InputAction.CallbackContext context) {
        if (_isRunOutOfStamina) {
            return;
        }
        _hasSprintInput = true;
    }
    private void EndSprint(InputAction.CallbackContext context) {
        _hasSprintInput = false;
    }
    private void EndAttack() {
        _isOnAttack = false;
    }
}