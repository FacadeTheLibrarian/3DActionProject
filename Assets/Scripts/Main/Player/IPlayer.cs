using UnityEngine;

internal interface IPlayer {
    Transform GetTransform();
    float GetScaleY();
    Vector3 GetForward();
    float GetAttackFactor();
}