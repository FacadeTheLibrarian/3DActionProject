using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;


//NOTE: Animator Event では同一の関数名を見分けることができない模様
//      そのため基底で Attack() を定義することができない
//      サブクラスで固有の名前を付ける必要がある
internal class BaseAttack : MonoBehaviour {

    [SerializeField] protected LayerMask _layer = default;
    [SerializeField, Range(0, 100)] protected int _baseDamage = default;
    [SerializeField] protected Vector3 _initialCastOffset = Vector3.zero;
    [SerializeField] protected Vector3 _boxSize = Vector3.one;

    protected IPlayer _player;
    public void Initialization(in IPlayer playerHandler) {
        _player = playerHandler;
        InnerInitializatiton();
    }

    protected virtual void InnerInitializatiton() {
        //nop
    }

    protected Vector3 GetInitialCastPosition() {
        Quaternion rotation = Quaternion.LookRotation(_player.GetForward(), Vector3.up);
        Vector3 offset = rotation * _initialCastOffset;
        return this.transform.position + offset;
    }
}