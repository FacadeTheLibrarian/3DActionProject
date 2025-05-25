using UnityEngine;

internal sealed class PlayerRoot : MonoBehaviour {
    [SerializeField] private Camera _mainCamera = default;
    [SerializeField] private MonsterData _monsterData = default;
    [SerializeField] private MonsterHandler _monsterHandler = default;
    [SerializeField] private AnimationStateController _animationController = default;

    [SerializeField] private PlayerGodClass _player = default;

    [SerializeField] private PlayerInputs _input = default;
    private PlayerDirection _direction = default;

    [SerializeField] private Vector3 _forward = default;

#if UNITY_EDITOR
    private void Reset() {
        _player = this.GetComponent<PlayerGodClass>();
    }
#endif

    public void PlayerStart() {
        //_monsterHandler.SummonMonsters(_monsterData, this.transform);
        //_animationController = new AnimationStateController(_monsterHandler);
        _player.PlayerStart();
    }
    public bool PlayerUpdate() {
        _player.HorizontalMove();
        _player.Dodge();
        _player.RecoverStamina();
        _player.UpdateStaminaBar();
        return false;
    }

    private void PlayerAwake() {
        for (int i = 0; i < (int)PlayerGeneration.e_generation.max; i++) {

        }
        //for (int i = 0; i < (int)BasePlayableMonster.e_generation.max; i++) {
        //    BasePlayableMonster monster = Instantiate(_monsterData[i], this.transform);
        //    _monsterHandler.AddMonster(monster);
        //    _monsterHandler[i].Initialization(this);

        //    _animator[i] = _monsterHandler[i].GetAnimator;

        //    //NOTE: 後方互換性のためにfalseになっているため、trueにするべきとのこと
        //    //      SEE -> https://www.youtube.com/watch?v=oF-nby5JBSw&t=251s ;
        //    _animator[i].keepAnimatorStateOnDisable = true;

        //    OnAttackBehaviour[] attackState = _animator[i].GetBehaviours<OnAttackBehaviour>();
        //    foreach (OnAttackBehaviour behaviour in attackState) {
        //        behaviour.Initialization(this);
        //        behaviour.OnStartAttackPublisher += AttackCast;
        //        behaviour.OnEndAttackPublisher += EndAttack;
        //    }

        //    if (i >= 1) {
        //        _monsterData[i].gameObject.SetActive(false);
        //    }
        //}
    }



    private void OnDestroy() {
    }
}