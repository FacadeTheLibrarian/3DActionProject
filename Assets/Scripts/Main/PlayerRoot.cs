using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//NOTE: maxは番兵
public enum e_inputActions {
    move = 0,
    sprint = 1,
    normalAttack = 2,
    specialAttack = 3,
    dodge = 4,
    max,
}

internal sealed class PlayerRoot : MonoBehaviour {

    private readonly string[] INPUT_ACTIONS_NAMES = new string[] {
        "Move",
        "Sprint",
        "NormalAttack",
        "SpecialAttack",
        "Dodge",
    };

    private const float UNITY_DEGREE_ADJUSTMENT = 90.0f;

    [SerializeField] private Camera _mainCamera = default;
    [SerializeField] private MonsterSetData _monsterHandler = default;
    [SerializeField] private PlayerGodClass _player = default;

    [SerializeField] private Vector3 _forward = default;
    [SerializeField] private InputActionAsset _actionAsset = default;

    [SerializeField] private InputAction[] _actions = new InputAction[(int)e_inputActions.max];

    

#if UNITY_EDITOR
    private void Reset() {
        _player = this.GetComponent<PlayerGodClass>();
    }
#endif

    public void PlayerStart() {
        _player.PlayerStart();
        for (int i = 0; i < (int)e_inputActions.max; i++) {
            _actions[i] = _actionAsset.FindAction(INPUT_ACTIONS_NAMES[i]);
        }
    }
    public bool PlayerUpdate() {
        //UpdateForward();
        _player.HorizontalMove();
        _player.Dodge();
        _player.RecoverStamina();
        _player.UpdateStaminaBar();
        return false;
    }

    private void UpdateForward() {
        Vector2 direction = _actions[(int)e_inputActions.move].ReadValue<Vector2>();
        //TODO: カメラがイベント発行者のリアクティブプロパティにしたらどうだろうか検討
        float cameraAngle = _mainCamera.transform.localRotation.eulerAngles.y;

        float radian = Mathf.Atan2(direction.y, -direction.x) + cameraAngle * Mathf.Deg2Rad;
        float degree = radian * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, degree - UNITY_DEGREE_ADJUSTMENT, this.transform.rotation.eulerAngles.z);

        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        _forward.Set(-x, 0.0f, y);
    }

    private void OnDestroy() {
    }
}