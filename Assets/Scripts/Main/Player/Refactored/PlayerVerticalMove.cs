using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerVerticalMove : MonoBehaviour {
    private const float FALL_SPEED_ADDEND = 9.81f;
    private const float VERTICAL_TERMINAL_VELOCITY = 10.0f;

    private CharacterController _characterController = default;
    private float _currentVelocity = 0.0f;

    public void Initialization(in CharacterController characterController) {
        _characterController = characterController;
    }

#if UNITY_EDITOR
    private void Reset() {
        _characterController = GetComponent<CharacterController>();
    }
#endif

    public Vector3 GetVerticalSpeed() {
        if (_characterController.isGrounded) {
            return Vector3.zero;
        }
        if (_currentVelocity < VERTICAL_TERMINAL_VELOCITY) {
            float nextVelocity = _currentVelocity + FALL_SPEED_ADDEND * Time.deltaTime;
            _currentVelocity = nextVelocity;
        }
        return Vector3.down * _currentVelocity;
    }
}