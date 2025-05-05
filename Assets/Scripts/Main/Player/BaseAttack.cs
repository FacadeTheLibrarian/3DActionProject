using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;


//NOTE: Animator Event では同一の関数名を見分けることができない模様
//      そのため基底で Attack() を定義することができない
//      サブクラスで固有の名前を付ける必要がある
internal class BaseAttack : MonoBehaviour {

    [SerializeField] protected LayerMask _layer = default;
    [SerializeField, Range(0, 100)] protected int _baseDamage = default;
    [SerializeField] protected Vector3 _castOffset = default;
    [SerializeField] protected Vector3 _boxSize = default;

    protected IPlayer _player;
    [SerializeField] protected PlayerGodClass _playerDebug;
    private void Awake() {
        _player = _playerDebug;
    }

    public void SetUp(in IPlayer playerHandler) {
        _player = playerHandler;
    }

    protected Vector3 GetCastPosition() {
        Quaternion rotation = Quaternion.LookRotation(_player.GetForward(), Vector3.up);
        Vector3 offset = rotation * _castOffset;
        return this.transform.position + offset;
    }
}