using UnityEngine;

internal sealed class PlayerDirection {
    private const float UNITY_DEGREE_ADJUSTMENT = 90.0f;

    private Transform _positionHandler = default;
    private Vector3 _forward = default;
    public Vector3 GetForward => _forward;
    public Vector3 GetRight {
        get {
            Vector3 right = new Vector3(_forward.z, 0.0f, -_forward.x);
            return right;
        }
    }
    public Vector3 GetLeft {
        get {
            Vector3 left = new Vector3(-_forward.z, 0.0f, _forward.x);
            return left;
        }
    }

    public PlayerDirection(in Transform handler) {
        _positionHandler = handler;
    }

    public void UpdateForward(Vector2 inputAxis) {
        float cameraAngle = Camera.main.transform.localRotation.eulerAngles.y;

        float radian = Mathf.Atan2(inputAxis.y, -inputAxis.x) + (cameraAngle * Mathf.Deg2Rad);
        float degree = radian * Mathf.Rad2Deg;

        _positionHandler.rotation = Quaternion.Euler(_positionHandler.rotation.eulerAngles.x, degree - UNITY_DEGREE_ADJUSTMENT, _positionHandler.rotation.eulerAngles.z);

        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        _forward.Set(-x, 0.0f, y);
    }
}