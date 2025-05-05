using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//NOTE: maxは番兵


internal sealed class PlayerRoot : MonoBehaviour {
    private const float UNITY_DEGREE_ADJUSTMENT = 90.0f;

    [SerializeField] private Camera _mainCamera = default;
    [SerializeField] private MonsterSetData _monsterHandler = default;
    [SerializeField] private PlayerGodClass _player = default;

    [SerializeField] private PlayerInputs _input = default;
    private PlayerDirection _direction;

    [SerializeField] private Vector3 _forward = default;



#if UNITY_EDITOR
    private void Reset() {
        _player = this.GetComponent<PlayerGodClass>();
    }
#endif

    public void PlayerStart() {
        //_direction = new PlayerDirection(this.transform);
        _player.PlayerStart();
    }
    public bool PlayerUpdate() {
        //_direction.UpdateForward(_input.GetAxis());
        _player.HorizontalMove();
        _player.Dodge();
        _player.RecoverStamina();
        _player.UpdateStaminaBar();
        return false;
    }

    private void OnDestroy() {
    }
}