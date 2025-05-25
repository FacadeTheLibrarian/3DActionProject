using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerDodge : MonoBehaviour, IMoveBehaviour {
    private const float DODGE_INITIAL_FORCE = 30.0f;
    private const float DODGE_TIME = 1 / 0.5f;

    public event Action OnEndDodge;

    private PlayerDirection _playerDirection = default;
    private PlayerStamina _playerStamina = default;
    private Vector3 _cachedForward = default;

    [SerializeField] private float _dodgeForce = DODGE_INITIAL_FORCE;
    private float _baseStaminaConsumption = 0.0f;
    private float _staminaConsumptionFactor = 1.0f;

    public void Initialization(in PlayerDirection direction, in PlayerStamina stamina, in MonsterData monsterData) {
        _playerDirection = direction;
        _playerStamina = stamina;
        _baseStaminaConsumption = monsterData.GetBaseStaminaConsumptionOnDodge;
    }

    public Vector3 UpdateMoveBehaviour(Vector2 inputDirection) {
        return Dodge(inputDirection);
    }

    public void OnStartDodge(Vector2 inputDirection) {
        float magnitude = Mathf.Clamp01(inputDirection.magnitude);
        _cachedForward = _playerDirection.GetUpdatedForward(inputDirection);
        _playerStamina.UseStamina(_baseStaminaConsumption * _staminaConsumptionFactor);
    }

    private Vector3 Dodge(Vector2 inputDirection) {
        
        Vector3 force = new Vector3(_cachedForward.x * _dodgeForce, 0.0f, _cachedForward.z * _dodgeForce);
        _dodgeForce -= DODGE_INITIAL_FORCE * DODGE_TIME * Time.deltaTime;

        //NOTE: この条件に入るときは「回避行動が終わった時」
        //      もう移動はしないので戻り値は無効が保証されている
        if (_dodgeForce <= 0.0f) {
            _dodgeForce = DODGE_INITIAL_FORCE;
            OnEndDodge?.Invoke();
            return Vector3.zero;
        }

        return force;
    }
}