using UnityEngine;

internal sealed class PlayerDirection : MonoBehaviour {
    private const float UNITY_DEGREE_ADJUSTMENT = 90.0f;

    [SerializeField] private Camera _mainCamera = default;
    private Vector3 _forward = default;
    public Vector3 GetConvert(in Vector3 forward){
            Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
            return right;
    }
    public Vector3 LeftConvert(in Vector3 forward) {
            Vector3 left = new Vector3(-forward.z, 0.0f, forward.x);
            return left;
    }

    public Vector3 GetForward(in Vector2 inputAxis) {
        float cameraAngle = _mainCamera.transform.localRotation.eulerAngles.y;

        float radian = Mathf.Atan2(inputAxis.y, -inputAxis.x) + (cameraAngle * Mathf.Deg2Rad);
        float degree = radian * Mathf.Rad2Deg;

        Vector3 eulerRotation = this.transform.eulerAngles;
        this.transform.rotation = Quaternion.Euler(eulerRotation.x, degree - UNITY_DEGREE_ADJUSTMENT, eulerRotation.z);

        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        _forward.Set(-x, 0.0f, y);
        return _forward;
    }
}