using UnityEngine;

//NOTE: Animator Event では同一の関数名を見分けることができない模様
//      そのため基底で Attack() を定義することができない
//      サブクラスで固有の名前を付ける必要がある
internal class BaseAttack : MonoBehaviour {

    [SerializeField] protected LayerMask _layer = (int)LayerNumbers.e_layers.enemy;
    [SerializeField, Range(0, 100)] protected int _baseDamage = default;
    [SerializeField] protected Vector3 _initialCastOffset = Vector3.zero;
    [SerializeField] protected Vector3 _boxSize = Vector3.one;

    protected Transform _playerTransform = default;
    protected PlayerDirection _direction = default;
    protected PlayerAttackFactor _attackFactor = default;

    public void Initialization(in Transform playerPosition, in PlayerDirection direction, in PlayerAttackFactor attackFactor) {
        _playerTransform = playerPosition;
        _direction = direction;
        _attackFactor = attackFactor;
        InnerInitializatiton();
    }
    protected virtual void InnerInitializatiton() {
        //nop
    }

    protected Vector3 GetInitialCastPosition() {
        Quaternion rotation = Quaternion.LookRotation(_direction.GetCachedForward(), Vector3.up);
        Vector3 offset = rotation * _initialCastOffset;
        return this.transform.position + offset;
    }
}